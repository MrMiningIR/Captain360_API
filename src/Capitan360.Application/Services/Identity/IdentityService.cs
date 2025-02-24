using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Capitan360.Application.Services.Identity.Users.Commands;
using Capitan360.Application.Services.Identity.Users.Queries;
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
using Microsoft.EntityFrameworkCore;

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
    IUserContext userContext) : IIdentityService
{
    public async Task<bool> ExistPhone(string phone, CancellationToken cancellationToken)
    {
        return await identityRepository.UserExistByPhone(phone, cancellationToken);
    }

    public async Task RegisterUser(CreateUserCommand createUserCommand, string roleName,
        CancellationToken cancellationToken)
    {

        logger.LogInformation("RegisterUser Function Called");
        var existUserByPone =
            await identityRepository.UserExistByPhone(createUserCommand.PhoneNumber, cancellationToken);

        if (existUserByPone)
            throw new UserAlreadyExistsException(createUserCommand.FullName, createUserCommand.PhoneNumber);
        var user = mapper.Map<User>(createUserCommand);

        if (string.IsNullOrEmpty(roleName))
            throw new NotFoundException($"{roleName.Normalize()} is not Passed!");
        var role = await roleManager.FindByNameAsync(roleName.Normalize())
                   ?? throw new NotFoundException(nameof(Role), roleName.Normalize());


        var result = await identityRepository.CreateUserAsync(user, cancellationToken);
        if (!result)
            throw new BadHttpRequestException("User Creation Failed");

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
            await userManager.UpdateAsync(user);
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