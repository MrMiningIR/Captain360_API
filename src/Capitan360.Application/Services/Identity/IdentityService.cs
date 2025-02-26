using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Entities.UserEntity;
using Capitan360.Domain.Exceptions;
using Capitan360.Domain.Repositories.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Capitan360.Domain.Abstractions;
using System.Data;
using Capitan360.Application.Services.Identity.Users;
using Capitan360.Application.Utils;
using Capitan360.Domain.Repositories.PermissionRepository;
using Microsoft.EntityFrameworkCore;
using Capitan360.Application.Services.Identity.Users.Commands.CreateUser;
using Capitan360.Application.Services.Identity.Users.Queries.LoginUser;
using Capitan360.Application.Services.Identity.Users.Queries.LogOut;
using Capitan360.Application.Services.Identity.Users.Responses;
using Capitan360.Application.Services.Identity.Users.Queries.RefreshToken;
using Capitan360.Application.Services.Identity.Users.Commands.AddUserGroup;
using Capitan360.Application.Services.Identity.Users.Queries.GetUserGroup;
using Capitan360.Application.Services.UserCompany;
using Capitan360.Application.Services.UserCompany.Commands.Create;
using Capitan360.Application.Services.UserCompany.Commands.Update;
using Capitan360.Application.Services.UserCompany.Queries;

namespace Capitan360.Application.Services.Identity;

internal class IdentityService(
    IIdentityRepository identityRepository,
    ILogger<IdentityService> logger,
    IMapper mapper,
    RoleManager<Role> roleManager,
    UserManager<User> userManager,
    IUserGroupRepository userGroupRepository,
    IConfiguration configuration,
    SignInManager<User> signInManager,
    ITokenService tokenService,
    IHttpContextAccessor httpContextAccessor,
    IRefreshTokenRepository refreshTokenRepository,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ITokenBlacklistsRepository tokenBlacklistsRepository) : IIdentityService
{
    public async Task<bool> ExistPhone(string phone, CancellationToken cancellationToken)
    {
        return await identityRepository.UserExistByPhone(phone, cancellationToken);
    }

    public async Task RegisterUser(CreateUserCommand createUserCommand, string roleName,
        CancellationToken cancellationToken)
    {


        logger.LogInformation("RegisterUser Function Called with This Parameter: @{CreateUserCommand}", createUserCommand);
        var existUserByPhone =
            await identityRepository.UserExistByPhone(createUserCommand.PhoneNumber, cancellationToken);

        if (existUserByPhone)
            throw new UserAlreadyExistsException(createUserCommand.FullName, createUserCommand.PhoneNumber);
        var user = mapper.Map<User>(createUserCommand);

        if (string.IsNullOrEmpty(roleName))
            throw new NotFoundException($"{roleName.Normalize()} is not Passed!");
        var role = await roleManager.FindByNameAsync(roleName.Normalize())
                   ?? throw new NotFoundException(nameof(Role), roleName.Normalize());


        var result = await identityRepository.CreateUserAsync(user, createUserCommand.Password, cancellationToken);
        if (result is { Succeeded: false })
            throw new BadHttpRequestException(string.Join(", ", result.Errors));


        await identityRepository.AddToRole(user, role);


    }

    public async Task<LoginResponse> LoginUser(LoginUserQuery loginUserQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("LoginUser Function Called with PhoneNumber: {PhoneNumber}", loginUserQuery.PhoneNumber);


        // Check if user exists and validate password in one step
        var user = await identityRepository.FindUserByPhone(loginUserQuery.PhoneNumber, cancellationToken);
        if (user == null || !await userManager.CheckPasswordAsync(user, loginUserQuery.Password))
            throw new UnAuthorizedException("Invalid phone number or password.");

        // Check for active session
        if (!string.IsNullOrEmpty(user.ActiveSessionId))
        {
            //TODO: Just For test


            var existingRefreshToken = await refreshTokenRepository.GetRefreshTokenByUserIdAndSessionId(user.Id, user.ActiveSessionId, cancellationToken);
            if (existingRefreshToken != null)
            {
                existingRefreshToken.IsRevoked = true;
                await unitOfWork.SaveChangesAsync(cancellationToken);
                // return BadRequest("User is already logged in");
            }
            user.ActiveSessionId = null;
            var identityResult = await userManager.UpdateAsync(user);
            if (!identityResult.Succeeded)
                throw new UnExpectedException("User update failed.");

            throw new ConflictException("User is already logged in from another session.");
        }

        // Generate new session ID and update user information
        string newSessionId = Tools.GenerateRandomSessionId();
        user.ActiveSessionId = newSessionId;
        user.LastAccess = DateTime.UtcNow;
        await userManager.UpdateAsync(user);

        // Get user groups and roles
        var userGroups = await userGroupRepository.GetUserGroupNameListAsyncByUserId(user.Id, cancellationToken);
        var roles = await userManager.GetRolesAsync(user) as IReadOnlyList<string>;


        var claims = tokenService.ClaimsGenerator(user, userGroups, roles!, newSessionId);
        var (resultToken, validTo) = tokenService.GenerateAccessToken(user, claims, RequiredKeys().key,
            RequiredKeys().issuer, RequiredKeys().audience);

        if (string.IsNullOrEmpty(resultToken))
            throw new UnExpectedException("Token Generation Failed");

        var refreshToken = tokenService.GenerateRefreshToken();
        var (encryptedRefreshToken, iv) = tokenService.EncryptRefreshToken(refreshToken, RequiredKeys().encryptionKey);
        var clientIp = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        await refreshTokenRepository.AddRefreshToken(new RefreshToken
        {
            UserId = user.Id,
            Token = encryptedRefreshToken,
            IV = iv,
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(7),
            IsRevoked = false,
            ClientIp = clientIp ?? "0.0.0.0",
            SessionId = newSessionId

        }, cancellationToken);
        var refreshTokenResult = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (refreshTokenResult <= 0)
            throw new UnExpectedException("Refresh Token Creation Failed");

        return new LoginResponse
        {
            AccessToken = resultToken,
            AccessTokenExpiration = validTo,
            SessionId = newSessionId,
            RefreshToken = refreshToken
        };
    }

    public async Task<TokenResponse> RefreshToken(RefreshTokenQuery refreshTokenQuery,
        CancellationToken cancellationToken)
    {

        logger.LogInformation("RefreshToken Function Called with This Parameter: @{refreshTokenQuery}", refreshTokenQuery);

        string? userId = GetUserIdFromExpiredToken().userId;
        if (string.IsNullOrEmpty(userId))
            throw new UnAuthorizedException("Invalid access token");



        var refreshTokenFromClient = refreshTokenQuery.RefreshToken;

        var refreshTokenFromDb = await refreshTokenRepository.GetRefreshTokenByUserIdAndSessionId(userId, GetUserIdFromExpiredToken().sessionId, cancellationToken) ??
                                 throw new UnAuthorizedException("Invalid or expired refresh token");

        if (refreshTokenFromDb is null)
            throw new UnAuthorizedException("No valid refresh token found for this user");

        var decryptedToken = tokenService.DecryptRefreshToken(refreshTokenFromDb.Token, refreshTokenFromDb.IV, RequiredKeys().encryptionKey);
        if (decryptedToken != refreshTokenFromClient)
            throw new UnAuthorizedException("Refresh token mismatch");


        var user = await userManager.FindByIdAsync(refreshTokenFromDb.UserId);
        if (user == null)
            throw new UnAuthorizedException("User not found");

        var clientIp = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

        //if (refreshToken.ClientIp != clientIp)
        //    throw new UnAuthorizedException("Refresh token used from different IP");

        var userGroups = await userGroupRepository.GetUserGroupNameListAsyncByUserId(user.Id, cancellationToken);
        var roles = await userManager.GetRolesAsync(user) as IReadOnlyList<string>;
        var claims = tokenService.ClaimsGenerator(user, userGroups, roles!,
            user.ActiveSessionId ?? throw new UnExpectedException("Active SessionId is null"));

        var newAccessToken = tokenService.GenerateAccessToken(user, claims, RequiredKeys().key,
            RequiredKeys().issuer, RequiredKeys().audience);
        var newRefreshToken = tokenService.GenerateRefreshToken();
        var (newEncryptedRefreshToken, newIv) = tokenService.EncryptRefreshToken(newRefreshToken, RequiredKeys().encryptionKey);

        refreshTokenFromDb.IsRevoked = true;

        await refreshTokenRepository.AddRefreshToken(new RefreshToken
        {
            UserId = user.Id,
            Token = newEncryptedRefreshToken,
            IV = newIv,
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(7),
            IsRevoked = false,
            ClientIp = clientIp ?? "0.0.0.0",
            SessionId = refreshTokenFromDb.SessionId
        }, cancellationToken);
        var refreshTokenResult = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (refreshTokenResult <= 0)
            throw new UnExpectedException("Refresh Token Creation Failed");

        return new TokenResponse(newAccessToken.resultToken, newRefreshToken, 15 * 60);


    }

    public async Task LogOutUser(LogOutQuery logOutQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("LogOutUser Function Called with This Parameter: @{logOutQuery}", logOutQuery);
        // Find the user by UserId
        var user = await userManager.FindByIdAsync(logOutQuery.UserId) ?? throw new NotFoundException("User not found.");

        // Check if the provided SessionId matches the ActiveSessionId
        if (user.ActiveSessionId != logOutQuery.SessionId)
            throw new UnAuthorizedException("Invalid session.");

        // Find the refresh token by UserId and SessionId
        var refreshToken = await refreshTokenRepository.GetRefreshTokenByUserIdAndSessionId(logOutQuery.UserId, logOutQuery.SessionId, cancellationToken)
                           ?? throw new NotFoundException("Refresh token not found.");

        // Revoke the refresh token
        refreshToken.IsRevoked = true;
        // Update the user's active session ID
        user.ActiveSessionId = null;
        var identityResult = await userManager.UpdateAsync(user);

        if (!identityResult.Succeeded)
            throw new UnExpectedException("User update failed.");

        // Add the token to the blacklist
        var tokenBlacklist = new TokenBlacklist
        {
            Token = logOutQuery.Token,
            ExpiryDate = DateTime.UtcNow.AddHours(24), // Set expiration based on token lifetime
            UserId = logOutQuery.UserId
        };
        await tokenBlacklistsRepository.AddAsync(tokenBlacklist, cancellationToken);
        var result = await unitOfWork.SaveChangesAsync(cancellationToken);
        if (result <= 0)
            throw new UnExpectedException("Token Blacklist Creation Failed");





    }

    public async Task AddUserToGroup(AddUserGroupCommand addUserGroupCommand, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(addUserGroupCommand.GroupId.ToString()) || string.IsNullOrEmpty(addUserGroupCommand.UserId))
            throw new NotFoundException("GroupId or UserId is not present");

        var existUserGroup = await userGroupRepository.GetUserGroupAsync(addUserGroupCommand.UserId, addUserGroupCommand.GroupId, cancellationToken);
        if (existUserGroup != null)
            throw new UserAlreadyExistsException("User is already in the group");

        var userGroup = mapper.Map<UserGroup>(addUserGroupCommand);

        await userGroupRepository.AddUerToGroup(userGroup, cancellationToken);

        var result = await unitOfWork.SaveChangesAsync(cancellationToken);
        if (result <= 0)
            throw new UnExpectedException("User Group Creation Failed");


    }

    public async Task RemoveUserFromGroup(GetUserGroupQuery getUserGroupQuery, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(getUserGroupQuery.GroupId.ToString()) || string.IsNullOrEmpty(getUserGroupQuery.UserId))
            throw new NotFoundException("GroupId or UserId is not present");

        //var userGroup = mapper.Map<UserGroup>(getUserGroupQuery);
        //userGroup.

        var userGroup =
           await userGroupRepository.GetUserGroupAsync(getUserGroupQuery.UserId, getUserGroupQuery.GroupId,
                cancellationToken) ?? throw new NotFoundException("User Group Not Found");

        userGroupRepository.RemoveUserFromGroup(userGroup, cancellationToken);
        var result = await unitOfWork.SaveChangesAsync(cancellationToken);
        if (result <= 0)
            throw new UnExpectedException("User Group Deletion Failed");





    }

    public async Task<IReadOnlyList<UserDto>> GetUsersByCompany(GetUsersByCompanyQuery getUserByCompanyQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all UsersByCompany");
        var users = await identityRepository.GetUsersByCompanyAsync(getUserByCompanyQuery.CompanyId, cancellationToken);

        var usersDto = mapper.Map<IReadOnlyList<UserDto>>(users);

        return usersDto;



    }

    public async Task<UserDto?> GetUserByCompany(GetUserByCompanyQuery getUsersByCompanyQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetUserByCompany Called with {@GetUserByCompanyQuery}", getUsersByCompanyQuery);

        var user = await identityRepository.GetUserByCompanyAsync(getUsersByCompanyQuery.UserId, getUsersByCompanyQuery.CompanyId, cancellationToken);
        var userDto = mapper.Map<UserDto>(user);
        return userDto;

    }

    public async Task<string> CreateUserByCompany(CreateUserCompanyCommand createUserByCompanyCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateUserByCompany Called with {@CreateUserCompanyCommand}", createUserByCompanyCommand);

        var user = mapper.Map<User>(createUserByCompanyCommand);
        var result = await userManager.CreateAsync(user, createUserByCompanyCommand.Password);
        if (result is { Succeeded: false })
            throw new BadHttpRequestException(string.Join(", ", result.Errors));


        return user.Id;



    }

    public async Task UpdateUserCompany(UpdateUserCompanyCommand updateUserCompanyCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateUserCompany Called with {@UpdateUserCompanyCommand}", updateUserCompanyCommand);
        var existingUser = await identityRepository.GetUserByCompanyAsync(updateUserCompanyCommand.UserId, updateUserCompanyCommand.CompanyId, cancellationToken)
                             ?? throw new NotFoundException("User not found");
        mapper.Map(updateUserCompanyCommand, existingUser);
        await unitOfWork.SaveChangesAsync(cancellationToken);


    }


    //Helper Functions
    private (string key, string issuer, string audience, string encryptionKey) RequiredKeys()
    {
        var key = configuration["Jwt:Key"] ?? throw new NotFoundException("The Jwt key not found");
        var issuer = configuration["Jwt:Issuer"] ?? throw new NotFoundException("The Issuer key not found");
        var audience = configuration["Jwt:Audience"] ?? throw new NotFoundException("The Audience key not found");
        var encryptionKey = configuration["EncryptionKey"] ??
                            throw new NotFoundException("The EncryptionKey  not found");
        return (key, issuer, audience, encryptionKey);
    }

    private (string? userId, string? sessionId) GetUserIdFromExpiredToken()
    {
        var authHeader = httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            throw new UnAuthorizedException("Access token required");

        var expiredToken = authHeader.Substring("Bearer ".Length).Trim();



        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(expiredToken);
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ??
                         throw new UnAuthorizedException("userId in not Present!");
            var sessionId = jwtToken.Claims.FirstOrDefault(c => c.Type == "SessionId")?.Value ??
                            throw new UnAuthorizedException("sessionId in not Present!");

            return (userId,
                sessionId);
        }
        catch
        {
            return (null, null);
        }
    }







}