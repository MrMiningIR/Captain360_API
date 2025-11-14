using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Addresses.Areas.Dtos;
using Capitan360.Application.Features.Companies.CompanyTypes.Dtos;
using Capitan360.Application.Features.Companies.UserCompany.Commands.Create;
using Capitan360.Application.Features.Companies.UserCompany.Commands.UpdateUserCompany;
using Capitan360.Application.Features.Companies.UserCompany.Queries.GetUserByCompany;
using Capitan360.Application.Features.Companies.UserCompany.Queries.GetUserById;
using Capitan360.Application.Features.Companies.UserCompany.Queries.GetUsersByCompany;
using Capitan360.Application.Features.Dtos;
using Capitan360.Application.Features.Identities.Identities.Commands.ChangePassword;
using Capitan360.Application.Features.Identities.Identities.Commands.ChangeUserActivity;
using Capitan360.Application.Features.Identities.Identities.Commands.CreateUser;
using Capitan360.Application.Features.Identities.Identities.Commands.UpdateUser;
using Capitan360.Application.Features.Identities.Identities.Queries.LoginUser;
using Capitan360.Application.Features.Identities.Identities.Queries.LogOut;
using Capitan360.Application.Features.Identities.Identities.Queries.RefreshToken;
using Capitan360.Application.Features.Identities.Identities.Responses;
using Capitan360.Application.Features.Identities.Permissions.Services;
using Capitan360.Application.Features.Identities.Roles.Roles.Dtos;
using Capitan360.Application.Features.Identities.Users.Users.Dtos;
using Capitan360.Application.Utils;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.Identities;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Exceptions;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Addresses;
using Capitan360.Domain.Interfaces.Repositories.Companies;
using Capitan360.Domain.Interfaces.Repositories.Identities;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ValidationException = FluentValidation.ValidationException;

namespace Capitan360.Application.Features.Identities.Identities.Services;

public class IdentityService(
    ILogger<IdentityService> logger,
    IMapper mapper,
    RoleManager<Domain.Entities.Identities.Role> roleManager,
    UserManager<User> userManager,
    IConfiguration configuration,
    SignInManager<User> signInManager,
    ITokenRepository tokenRepository,
    IHttpContextAccessor httpContextAccessor,
    IRefreshTokenRepository refreshTokenRepository,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    IPermissionService permissionService,
    ICompanyRepository companyRepository,
    IUserPermissionRepository userPermissionRepository,
    IAreaRepository areaRepository,
    IUserRepository userRepository,
    ICompanyTypeRepository companyTypeRepository,
    IMultiTenantContextAccessor<TenantInfo> tenantContext,
    ICompanyUriRepository companyUriRepository

    ) : IIdentityService
{
    public async Task<ApiResponse<string>> RegisterUser(CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("RegisterUser Function Called with This Parameter: @{CreateUserCommand}", command);

        var existedUser = await userRepository.GetUserByMobileAndCompanyIdAsync(command.PhoneNumber, command.CompanyId, false, true, cancellationToken);

        if (existedUser is not null)
            return ApiResponse<string>.Error(400, $"کاربر با این شماره قبلا ثبت شده است {command.PhoneNumber}");

        var user = mapper.Map<User>(command);

        if (user == null)
            return ApiResponse<string>.Error(500, "مشکل در عملیات تبدیل");

        if (string.IsNullOrEmpty(command.RoleId) || string.IsNullOrEmpty(command.RoleName))
            return ApiResponse<string>.Error(400, $"نقش کاربری انتخاب نشده است");

        var role = await roleManager.FindByIdAsync(command.RoleId);

        if (role is null)
            return ApiResponse<string>.Error(400, $"نقشی با این مشخصات وجود دارد {command.RoleName}");


        var userCompany =
            await companyRepository.GetCompanyByIdAsync(command.CompanyId, false, false, cancellationToken);

        if (userCompany is null)
            return ApiResponse<string>.Error(400, $"شرکت انتخابی نامعتبر است");


        logger.LogInformation("RoleName successfully Found: {Id}", role.NormalizedName);

        // Start Atomic Operation
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        user.Tell = command.PhoneNumber;
        user.PermissionVersion = Guid.NewGuid().ToString();
        user.TypeOfFactorInSamanehMoadianId = (short)command.TypeOfFactorInSamanehMoadianId;
        user.CompanyTypeId = userCompany.CompanyTypeId;
        user.PhoneNumber = command.PhoneNumber;
        EnsureUserStringDefaults(user);

        var result = await userRepository.CreateUserAsync(user, command.Password, role, cancellationToken);
        if (result.Result is { Succeeded: false })
            return ApiResponse<string>.Error(400, string.Join(", ", result.Result.Errors.Select(x => x.Description)));

        logger.LogInformation("User successfully Created: {Id}", user.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await unitOfWork.CommitTransactionAsync(cancellationToken);
        logger.LogInformation("User created successfully with ID: {Id}", user.Id);
        return ApiResponse<string>.Created(user.Id, "User created successfully");
    }

    private static void EnsureUserStringDefaults(User user)
    {

        user.AccountCodeInDesktopCaptainCargo ??= string.Empty;
        user.MobileTelegram ??= string.Empty;
        user.NationalCode ??= string.Empty;
        user.EconomicCode ??= string.Empty;
        user.NationalId ??= string.Empty;
        user.RegistrationId ??= string.Empty;
        user.ActivationCode ??= string.Empty;
        user.RecoveryPasswordCode ??= string.Empty;
        user.ActiveSessionId ??= string.Empty;

    }

    public async Task<ApiResponse<string>> UpdateUser(UpdateUserCommand command,
    CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("UpdateUser Function Called with This Parameter: @{UpdateUserCommand}", command);

        var existUserById = await userRepository.GetUserByIdAsync(command.UserId, false, true, cancellationToken);

        if (existUserById is null)
            return ApiResponse<string>.Error(400, $"کاربر با این مشخصات وجود ندارد {command.UserId}");

        if (existUserById.UserName != command.PhoneNumber)
        {
            // Check if phone number already exists in the same company
            var phoneExists = await userRepository.CheckExistUserMobileAsync(command.PhoneNumber, existUserById.CompanyId, existUserById.Id, cancellationToken);
            
            if (phoneExists)
            {
                return ApiResponse<string>.Error(400, $"شماره تلفن قبلاً توسط کاربر دیگری در این شرکت استفاده شده است.");
            }
        }

        // Start Atomic Operation
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var updatedUser = mapper.Map(command, existUserById);

        if (updatedUser == null)
            return ApiResponse<string>.Error(400, "مشکل در عملیات تبدیل");

        var result = await userManager.UpdateAsync(updatedUser);

        if (result is { Succeeded: false })
            return ApiResponse<string>.Error(400, string.Join(", ", result.Errors));

        logger.LogInformation("User successfully Updated: {Id}", existUserById.Id);

        if (!string.IsNullOrEmpty(command.RoleId) && !string.IsNullOrEmpty(command.RoleName))
        {
            var requestedRole = await roleManager.FindByIdAsync(command.RoleId!);

            if (requestedRole is null)
                return ApiResponse<string>.Error(400, $"نقشی با این مشخصات وجود دارد {command.RoleName}");

            var checkRoleExistsInUser = await userManager.IsInRoleAsync(existUserById, requestedRole.NormalizedName!);
            if (!checkRoleExistsInUser)
            {
                var currentRoles = await userManager.GetRolesAsync(existUserById);
                if (currentRoles.Any())
                {
                    await userManager.RemoveFromRolesAsync(existUserById, currentRoles);
                }

                await userManager.AddToRoleAsync(existUserById, requestedRole.NormalizedName!);
            }

            logger.LogInformation("RoleName successfully Found: {Id}", requestedRole.NormalizedName);
        }

        existUserById.PermissionVersion = Guid.NewGuid().ToString();
        existUserById.TypeOfFactorInSamanehMoadianId = (short)command.TypeOfFactorInSamanehMoadianId;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await unitOfWork.CommitTransactionAsync(cancellationToken);
        logger.LogInformation("User Updated successfully with ID: {Id}", existUserById.Id);
        return ApiResponse<string>.Updated(existUserById.Id);
    }

    public async Task<ApiResponse<LoginResponse>> LoginUser(LoginUserQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("LoginUser Function Called with PhoneNumber: {PhoneNumber}", query.PhoneNumber);

        var tenantInfo = tenantContext.MultiTenantContext?.TenantInfo;

        if (tenantInfo is null || string.IsNullOrEmpty(tenantInfo.Identifier))
        {
            return ApiResponse<LoginResponse>.Error(401, "نام کاریری یا رمز عبور صحیح نیست");
        }

        // First, get CompanyId from URI
        var companyUri = await companyUriRepository.GetUriByTenant(tenantInfo.Identifier, cancellationToken);
        if (companyUri is null)
            return ApiResponse<LoginResponse>.Error(401, "نام کاریری یا رمز عبور صحیح نیست");

        // Now use tenant-aware FindByNameAsync (it will filter by CompanyId automatically)
        var checkUser = await userManager.FindByNameAsync(query.PhoneNumber);

        if (checkUser == null)
        {
            return ApiResponse<LoginResponse>.Error(401, "نام کاریری یا رمز عبور صحیح نیست");
        }

        // Verify the user belongs to the company from URI
        if (checkUser.CompanyId != companyUri.CompanyId)
        {
            return ApiResponse<LoginResponse>.Error(401, "نام کاریری یا رمز عبور صحیح نیست");
        }

        logger.LogInformation("LoginUser Function Called with PhoneNumber: {PhoneNumber}", query.PhoneNumber);

        // Load user with full data (including Company and Roles)
        var user = await userRepository.GetUserByMobileAndCompanyIdAsync(query.PhoneNumber, companyUri.CompanyId, true, true, cancellationToken);

        if (user == null)
        {
            return ApiResponse<LoginResponse>.Error(401, "نام کاریری یا رمز عبور صحیح نیست");
        }

        if (await userManager.IsLockedOutAsync(user))
        {
            await userManager.ResetAccessFailedCountAsync(user);

            await userManager.SetLockoutEndDateAsync(user, null);
        }

        var result = await signInManager.PasswordSignInAsync(
        userName: query.PhoneNumber,
        password: query.Password,
        isPersistent: false,
        lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
            {
                return ApiResponse<LoginResponse>.Error(403, "حساب شما به دلیل تلاش‌های ناموفق قفل شده است.");
            }
            if (result.RequiresTwoFactor)
            {
                return ApiResponse<LoginResponse>.Error(401, "نیاز به احراز هویت دو مرحله‌ای است.");
            }

            return ApiResponse<LoginResponse>.Error(401, "نام کاریری یا رمز عبور صحیح نیست");
        }

        if (!user.Active)

            return ApiResponse<LoginResponse>.Error(401, ConstantNames.DeactivatedAccountMessage);

        // Check for active session

        #region MyRegion

        //if (!string.IsNullOrEmpty(user!.ActiveSessionId))
        //{
        //await unitOfWork.BeginTransactionAsync(cancellationToken);

        //    //TODO: Just For test

        //    var existingRefreshToken = await refreshTokenRepository.GetRefreshTokenByUserIdAndSessionId(user.Id, user.ActiveSessionId, cancellationToken);
        //    if (existingRefreshToken != null)
        //    {
        //        //existingRefreshToken.IsRevoked = true;
        //        await refreshTokenRepository.DeleteRefreshToken(existingRefreshToken, cancellationToken);
        //        //await unitOfWork.SaveChangesAsync(cancellationToken);
        //        // return BadRequest("User is already logged in");
        //    }
        //    user.ActiveSessionId = null;
        //    var identityResult = await userManager.UpdateAsync(user);
        //    if (!identityResult.Succeeded)
        //        return ApiResponse<LoginResponse>.Error(500, "User Update failed.");

        //    await unitOfWork.SaveChangesAsync(cancellationToken);
        //    await unitOfWork.CommitTransactionAsync(cancellationToken);
        //    throw new ForbiddenForceLogoutException(ConstantNames.UserAlreadyLoggined);
        //}

        #endregion MyRegion

        await unitOfWork.BeginTransactionAsync(cancellationToken);
        // Generate new session ID and update user information
        string newSessionId = Tools.GenerateRandomSessionId();
        user.ActiveSessionId = newSessionId;
        user.LastAccess = DateTime.UtcNow;
        await userManager.UpdateAsync(user);

        // Get user groups and roles
        // var userGroups = await userGroupRepository.GetUserGroupNameListAsyncByUserId(user.Id, cancellationToken);
        var roles = await userManager.GetRolesAsync(user) as IReadOnlyList<string>;
        var permissions = await userPermissionRepository.GetUserPermissionsByUserId(user.Id, cancellationToken);

        if (roles is null || roles.Count <= 0)
            return ApiResponse<LoginResponse>.Error(401, "حساب کاربری شما معتبر نیست");

        if (!permissions.Any())
        {
            if (!roles.Contains(ConstantNames.SuperAdminRole))
            {
                return ApiResponse<LoginResponse>.Error(401, "حساب کاربری شما معتبر نیست");
            }
        }

        var claims = tokenRepository.ClaimsGenerator(user, roles!, newSessionId, permissions);
        var (resultToken, validTo) = tokenRepository.GenerateAccessToken(claims, RequiredKeys().key,
            RequiredKeys().issuer, RequiredKeys().audience);

        if (string.IsNullOrEmpty(resultToken))
            return ApiResponse<LoginResponse>.Error(500, "Token Generation Failed.");

        var refreshToken = tokenRepository.GenerateRefreshToken();
        var (encryptedRefreshToken, iv) = tokenRepository.EncryptRefreshToken(refreshToken, RequiredKeys().encryptionKey);
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
            SessionId = newSessionId,
            // PermissionVersionControl = permissionVersionControl
        }, cancellationToken);
        var refreshTokenResult = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (refreshTokenResult <= 0)
            return ApiResponse<LoginResponse>.Error(500, "Refresh Token Creation Failed");

        //var userPermissions = await permissionService.GetUserPermissions(user.Id, cancellationToken);

        await unitOfWork.CommitTransactionAsync(cancellationToken);
        var data = new LoginResponse
        {
            UserName = query.PhoneNumber,
            AccessToken = resultToken,
            AccessTokenExpiration = validTo,
            SessionId = newSessionId,
            RefreshToken = refreshToken,
            SystemPermissions = [],
            PermissionVersionControl = user.PermissionVersion
        };

        return ApiResponse<LoginResponse>.Ok(data, "User LoggedIn successfully");
    }

    public async Task<ApiResponse<TokenResponse>> RefreshToken(RefreshTokenQuery query,
     CancellationToken cancellationToken)
    {
        logger.LogInformation("RefreshToken Function Called with This Parameter: @{refreshTokenQuery}", query);

        string? userId = GetUserIdFromExpiredToken().userId;
        if (string.IsNullOrEmpty(userId))
            return ApiResponse<TokenResponse>.Error(401, "Invalid access token.");

        var refreshTokenFromClient = query.RefreshToken;
        var sessionId = GetUserIdFromExpiredToken().sessionId;
        if (string.IsNullOrEmpty(sessionId))
            return ApiResponse<TokenResponse>.Error(401, "Invalid access token or Session Id");

        var refreshTokenFromDb =
            await refreshTokenRepository.GetRefreshTokenByUserIdAndSessionId(userId,
                GetUserIdFromExpiredToken().sessionId!, cancellationToken);

        if (refreshTokenFromDb is null)
            return ApiResponse<TokenResponse>.Error(401, "Invalid or expired refresh token.");

        if (refreshTokenFromDb is null)
            throw new UnAuthorizedException("No valid refresh token found for this user");

        var decryptedToken = tokenRepository.DecryptRefreshToken(refreshTokenFromDb.Token, refreshTokenFromDb.IV, RequiredKeys().encryptionKey);
        if (decryptedToken != refreshTokenFromClient)
            return ApiResponse<TokenResponse>.Error(401, "Refresh token mismatch.");

        var user = await userManager.FindByIdAsync(refreshTokenFromDb.UserId);

        if (user == null)
            return ApiResponse<TokenResponse>.Error(401, "User not found.");

        var clientIp = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

        //if (refreshToken.ClientIp != clientIp)
        //    throw new UnAuthorizedException("Refresh token used from different IP");

        // var userGroups = await userGroupRepository.GetUserGroupNameListAsyncByUserId(user.Id, cancellationToken);
        var roles = await userManager.GetRolesAsync(user) as IReadOnlyList<string>;

        if (string.IsNullOrEmpty(user.ActiveSessionId))
            return ApiResponse<TokenResponse>.Error(401, "Active SessionId is null.");

        var permissions = await userPermissionRepository.GetUserPermissionsByUserId(user.Id, cancellationToken);

        if (roles is null || roles.Count <= 0)
            return ApiResponse<TokenResponse>.Error(401, "حساب کاربری شما معتبر نیست");

        if (!permissions.Any())
        {
            if (!roles.Contains(ConstantNames.SuperAdminRole))
            {
                return ApiResponse<TokenResponse>.Error(401, "حساب کاربری شما معتبر نیست");
            }
        }

        //var claims = tokenService.ClaimsGenerator(user, userGroups, roles!, user.ActiveSessionId);
        var claims = tokenRepository.ClaimsGenerator(user, roles!, user.ActiveSessionId, permissions);

        var newAccessToken = tokenRepository.GenerateAccessToken(claims, RequiredKeys().key,
            RequiredKeys().issuer, RequiredKeys().audience);
        var newRefreshToken = tokenRepository.GenerateRefreshToken();
        var (newEncryptedRefreshToken, newIv) = tokenRepository.EncryptRefreshToken(newRefreshToken, RequiredKeys().encryptionKey);

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
            return ApiResponse<TokenResponse>.Error(500, "Refresh Token Creation Failed.");

        return ApiResponse<TokenResponse>.Ok(new TokenResponse(newAccessToken.resultToken, newRefreshToken, user.PermissionVersion, 15 * 60), "Refresh Token Successfully Created");
    }

    public async Task LogOutUser(LogOutQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("LogOutUser Function Called with This Parameter: @{logOutQuery}", query);
        // Find the user by UserId
        var user = await userManager.FindByIdAsync(query.UserId) ?? throw new NotFoundException("User not found.");

        // Check if the provided SessionId matches the ActiveSessionId
        if (user.ActiveSessionId != query.SessionId)
            throw new UnAuthorizedException("Invalid session.");

        // Find the refresh token by UserId and SessionId
        var refreshToken = await refreshTokenRepository.GetRefreshTokenByUserIdAndSessionId(query.UserId, query.SessionId, cancellationToken)
                           ?? throw new NotFoundException("Refresh token not found.");
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        // Revoke the refresh token
        refreshToken.IsRevoked = true;
        // Update the user's active session ID
        user.ActiveSessionId = null;
        var identityResult = await userManager.UpdateAsync(user);

        if (!identityResult.Succeeded)
            throw new UnExpectedException("User update failed.");

        var result = await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);
        if (result <= 0)
            throw new UnExpectedException("Token Blacklist Creation Failed");
    }

    public async Task<ApiResponse<PagedResult<UserDto>>> GetUsersByCompany(GetUsersQuery query,
        CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("Getting all UsersByCompany");
        if (query.PageSize <= 0 || query.PageNumber <= 0)
            return ApiResponse<PagedResult<UserDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

        var validator = new GetUsersByCompanyQueryValidator();
        var validationResult = await validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }



        var (users, totalCount) = await userRepository.GetAllUsersAsync(query.SearchPhrase, query.SortBy, query.CompanyId,
            !user.IsSuperAdmin() ? user.CompanyType : query.CompanyType, query.RoleId, query.ModaianFactorTypeId, query.HasCredit
            , query.Banned, query.Active, query.IsBike, true, query.PageNumber, query.PageSize, query.SortDirection,
            cancellationToken);

        var usersDto = mapper.Map<IReadOnlyList<UserDto>>(users) ?? Array.Empty<UserDto>();
        logger.LogInformation("Retrieved {Count} areas", usersDto.Count);
        var data = new PagedResult<UserDto>(usersDto, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<UserDto>>.Ok(data, "Areas retrieved successfully");
    }

    public async Task<ApiResponse<UserDto>> GetUserByCompany(GetUserByCompanyQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetUserByCompany Called with {@GetUserByCompanyQuery}", query);
        var currentUser = userContext.GetCurrentUser();

        //if (currentUser is null || !currentUser.IsManager())
        //    return ApiResponse<UserDto>.Error(403, $"مجوز انجام این عملیات را ندارید.");

        //query.CompanyId = currentUser!.CompanyId;

        var validator = new GetUserByCompanyQueryValidator();
        var validationResult = await validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var user = await userRepository.GetUserByIdAndCompanyId(query.UserId, query.CompanyId, false, true, cancellationToken);

        if (user is null)
            return ApiResponse<UserDto>.Error(400, $";کاربر با شناسه {query.UserId} یافت نشد");

        var userDto = mapper.Map<UserDto>(user);
        logger.LogInformation("User retrieved successfully with ID: {Id}", query.UserId);

        return ApiResponse<UserDto>.Ok(userDto, "Area retrieved successfully");
    }

    public async Task<ApiResponse<UserDto>> GetUserById(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetUserById Called with {@GetUserByIdQuery}", query);
        var currentUser = userContext.GetCurrentUser();

        if (string.IsNullOrEmpty(query.UserId))
            return ApiResponse<UserDto>.Error(400, $" شناسه {query.UserId} یافت نشد");

        var user = await userRepository.GetUserByIdAsync(query.UserId, true, false, cancellationToken);
        if (user is null)
            return ApiResponse<UserDto>.Error(400, $"کاربر با شناسه {query.UserId} یافت نشد");

        var userDto = mapper.Map<UserDto>(user);
        logger.LogInformation("User retrieved successfully with ID: {Id}", query.UserId);

        return ApiResponse<UserDto>.Ok(userDto, "User retrieved successfully");
    }

    public async Task<ApiResponse<string>> CreateUserByCompany(CreateUserCompanyCommand? command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateUserByCompany Called with {@CreateUserCompanyCommand}", command);
        if (command == null)
            return ApiResponse<string>.Error(400, "ورودی ایجاد منطقه نمی‌تواند null باشد");

        var user = mapper.Map<User>(command);

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var result = await userManager.CreateAsync(user, command.Password);
        if (!result.Succeeded)
            return ApiResponse<string>.Error(400, $"خطا در ساخت کاربرجدید{string.Join(", ", result.Errors)}");

        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("User created successfully with ID: {UserId}", user.Id);

        return ApiResponse<string>.Created(user.Id, $"User created successfully : {user.Id}");
    }

    public async Task UpdateUserCompany(UpdateUserCompanyCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateUserCompany Called with {@UpdateUserCompanyCommand}", command);
        var existingUser = await userRepository.GetUserByIdAndCompanyId(command.UserId, command.CompanyId, false
            , true, cancellationToken)
                             ?? throw new NotFoundException("User not found");
        mapper.Map(command, existingUser);
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

    public ApiResponse<PagedResult<MoadianItemDto>> GeMoadianList()
    {
        var enumList = Enum.GetValues(typeof(MoadianFactorType))
                     .Cast<MoadianFactorType>()
                     .Select(e => new MoadianItemDto()
                     {
                         Value = (int)e,
                         Name = Tools.GetEnumDisplayName(e)
                     })
                     .ToList();

        var data = new PagedResult<MoadianItemDto>(enumList, enumList.Count, 10, 1);
        return ApiResponse<PagedResult<MoadianItemDto>>.Ok(data, "Areas retrieved successfully");
    }

    public ApiResponse<PagedResult<EntranceFeeTypeDto>> GetEntranceTypeList()
    {
        var enumList = Enum.GetValues(typeof(EntranceFeeType))
              .Cast<EntranceFeeType>()
              .Select(e => new EntranceFeeTypeDto()
              {
                  Value = (int)e,
                  Name = Tools.GetEnumDisplayName(e)
              })
              .ToList();

        var data = new PagedResult<EntranceFeeTypeDto>(enumList, enumList.Count, 10, 1);
        return ApiResponse<PagedResult<EntranceFeeTypeDto>>.Ok(data, "Areas retrieved successfully");
    }

    public ApiResponse<PagedResult<PathStructTypeDto>> GetPathStructTypeList()
    {
        var enumList = Enum.GetValues(typeof(PathStructType))
       .Cast<PathStructType>()
       .Select(e => new PathStructTypeDto()
       {
           Value = (int)e,
           Name = Tools.GetEnumDisplayName(e)
       })
       .ToList();

        var data = new PagedResult<PathStructTypeDto>(enumList, enumList.Count, 15, 1);
        return ApiResponse<PagedResult<PathStructTypeDto>>.Ok(data, "Areas retrieved successfully");
    }

    //public ApiResponse<PagedResult<WeightTypeDto>> GetWeightTypeList(List<int>? shouldRemove = null)
    //{
    //            var enumList = Enum.GetValues(typeof(WeightType))
    //    .Cast<WeightType>()
    //    .Select(e => new WeightTypeDto()
    //    {
    //        Value = (int)e,
    //        Name = Tools.GetEnumDisplayName(e)
    //    })
    //    .ToList();

    //    var data = new PagedResult<WeightTypeDto>(enumList, enumList.Count, 15, 1);
    //    return ApiResponse<PagedResult<WeightTypeDto>>.Ok(data, "Areas retrieved successfully");
    //}
    public ApiResponse<PagedResult<WeightTypeDto>> GetWeightTypeList(List<int>? shouldRemove = null)
    {
        var enumList = Enum.GetValues(typeof(WeightType))
            .Cast<WeightType>()
            .Select(e => new WeightTypeDto()
            {
                Value = (int)e,
                Name = Tools.GetEnumDisplayName(e)
            })
            .Where(e => shouldRemove == null || !shouldRemove.Contains(e.Value))
            .ToList();

        var data = new PagedResult<WeightTypeDto>(enumList, enumList.Count, 15, 1);
        return ApiResponse<PagedResult<WeightTypeDto>>.Ok(data, "Areas retrieved successfully");
    }

    public ApiResponse<PagedResult<RateDto>> GetRateList()
    {
        var enumList = Enum.GetValues(typeof(Rate))
.Cast<Rate>()
.Select(e => new RateDto()
{
    Value = (int)e,
    Name = Tools.GetEnumDisplayName(e)
})
.ToList();

        var data = new PagedResult<RateDto>(enumList, enumList.Count, 15, 1);
        return ApiResponse<PagedResult<RateDto>>.Ok(data, "Rates retrieved successfully");
    }

    public async Task<ApiResponse<PagedResult<RoleDto>>> GetRoles(CancellationToken cancellationToken)
    {
        //.Select(x => new RoleDto()
        //{
        //    Id = x.Id,
        //    RoleName = x.NormalizedName!,
        //    RolePersianName = x.PersianName ?? string.Empty,
        //    Visible = x.Visible
        //}

        var roles = await roleManager.Roles.ToListAsync(cancellationToken);

        var rolesDto = mapper.Map<List<RoleDto>>(roles);

        var data = new PagedResult<RoleDto>(rolesDto, roles.Count, 10, 1);

        return new ApiResponse<PagedResult<RoleDto>>(200, "Roles are Here !", data);
    }

    public async Task<ApiResponse<List<CompanyItemDto>>> GetCompaniesList(int companyTypeId,
        CancellationToken cancellationToken)
    {
        var companies = await companyRepository.GetAllCompaniesAsync(companyTypeId, cancellationToken);
        var companyItemDtos = companies.Select(x => new CompanyItemDto()
        {
            Id = x.Id,
            CompanyName = x.Name!,
            CompanyTypeId = x.CompanyTypeId,
            IsParentCompany = x.IsParentCompany
        }).ToList();

        return new ApiResponse<List<CompanyItemDto>>(200, "Companies", companyItemDtos);
    }

    public async Task<ApiResponse<List<CityAreaDto>>> GetCityList(CancellationToken cancellationToken)
    {
        var cities = await areaRepository.GetAllCities(cancellationToken);

        var cityAreaDtoList = cities.Select(x => new CityAreaDto()
        {
            Id = x.Id,
            PersianName = x.PersianName
        }).ToList();

        return new ApiResponse<List<CityAreaDto>>(200, "Cities", cityAreaDtoList);
    }

    public async Task<ApiResponse<string>> ChangePassword(ChangePasswordCommand command)
    {
        logger.LogInformation("ChangePassword Called with {@ChangePasswordCommand}", command);

        var user = await userManager.FindByIdAsync(command.UserId);
        if (user is null)
            return ApiResponse<string>.Error(400, $"User Data not Found :{command.UserId}");

        // بررسی اینکه رمز جدید با رمز فعلی یکسان نباشد
        var passwordVerificationResult = userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, command.NewPassword);
        if (passwordVerificationResult == PasswordVerificationResult.Success)
            return ApiResponse<string>.Error(400, $"رمز جدید نمی‌تواند با رمز قبلی یکسان باشد : {command.UserId}");

        // هش کردن رمز جدید
        var newHashedPassword = userManager.PasswordHasher.HashPassword(user, command.NewPassword);

        // به‌روزرسانی رمز هش‌شده کاربر
        user.PasswordHash = newHashedPassword;

        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return ApiResponse<string>.Error(500, $"ChangePassword was not succeeded : {command.UserId}");

        logger.LogInformation("User Updated successfully with ID: {Id}", command.UserId);
        return ApiResponse<string>.Ok($"ChangePassword was succeeded : {command.UserId}");
    }

    public async Task<ApiResponse<string>> SetUserActivityStatus(ChangeUserActivityCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("SetUserActivityStatus Called with {@ChangeUserActivityCommand}", command);

        var user = await userManager.FindByIdAsync(command.UserId);
        if (user is null)
            return ApiResponse<string>.Error(400, $"User Data not Found :{command.UserId}");

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        user.Active = !user.Active;
        user.PermissionVersion = Guid.NewGuid().ToString();

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
            return ApiResponse<string>.Error(500, $"SetUserActivityStatus  was not succeeded : {command.UserId}");

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("SetUserActivityStatus Updated successfully with ID: {Id}", command.UserId);
        return ApiResponse<string>.Ok($"SetUserActivityStatus  was succeeded : {command.UserId}");
    }

    public async Task<ApiResponse<List<CompanyTypeDto>>> GetAllCompanyTypes(CancellationToken cancellationToken)
    {
        var companyTypes = await companyTypeRepository.GetAllCompanyTypes(cancellationToken);

        var companyTypesDto = companyTypes.Select(x => new CompanyTypeDto()
        {
            Id = x.Id,
            DisplayName = x.DisplayName
        }).ToList();

        return new ApiResponse<List<CompanyTypeDto>>(200, "CompanyTypes", companyTypesDto);
    }
}