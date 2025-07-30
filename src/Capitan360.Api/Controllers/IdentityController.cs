using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Services.Dtos;
using Capitan360.Application.Services.Identity.Commands.ChangePassword;
using Capitan360.Application.Services.Identity.Commands.ChangeUserActivity;
using Capitan360.Application.Services.Identity.Commands.CreateUser;
using Capitan360.Application.Services.Identity.Commands.UpdateUser;
using Capitan360.Application.Services.Identity.Dtos;
using Capitan360.Application.Services.Identity.Queries.LoginUser;
using Capitan360.Application.Services.Identity.Queries.LogOut;
using Capitan360.Application.Services.Identity.Queries.RefreshToken;
using Capitan360.Application.Services.Identity.Responses;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Application.Services.UserCompany.Commands.Create;
using Capitan360.Application.Services.UserCompany.Queries.GetUserById;
using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Entities.UserEntity;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[PermissionFilter("بخش کاربران", "T")]
public class IdentityController(UserManager<User> userManager,
    RoleManager<Role> roleManager,
  IConfiguration configuration,
    IIdentityService identityService, IMultiTenantContextAccessor<TenantInfo> tenantContext) : ControllerBase

{
    [HttpPost("Register")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("ثبت کاربر", "T1")]
    public async Task<ActionResult<ApiResponse<string>>> Register([FromBody] CreateUserCommand createUserCommand, CancellationToken cancellationToken)
    {
        var response = await identityService.RegisterUser(createUserCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("UpdateUser")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    [PermissionFilter("آپدیت کاربر", "T2")]
    public async Task<ActionResult<ApiResponse<string>>> UpdateUser([FromBody] UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
    {
        var response = await identityService.UpdateUser(updateUserCommand, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("ChangePassword")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [PermissionFilter("تغییر پسورد کاربر", "T3")]
    public async Task<ActionResult<ApiResponse<string>>> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        var response = await identityService.ChangePassword(command);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("ChangeUserActiveStatus")]
    [PermissionFilter("تغییر وضعیت کاربر", "T4")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<string>>> ChangeUserActiveStatus([FromBody] ChangeUserActivityCommand command, CancellationToken cancellationToken)
    {
        var response = await identityService.SetUserActivityStatus(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("Login")]
    [ExcludeFromPermission]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<LoginResponse>>> Login([FromBody] LoginUserQuery loginUserQuery, CancellationToken cancellationToken)
    {
        var tenant = tenantContext?.MultiTenantContext?.TenantInfo?.Id;

        var response = await identityService.LoginUser(loginUserQuery, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    // POST: api/identity/login
    [HttpPost("Refresh")]
    [ProducesResponseType(typeof(ApiResponse<TokenResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<TokenResponse>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<TokenResponse>), StatusCodes.Status500InternalServerError)]
    [ExcludeFromPermission]
    public async Task<ActionResult<TokenResponse>> Refresh(RefreshTokenQuery refreshTokenQuery, CancellationToken cancellationToken)
    {
        var response = await identityService.RefreshToken(refreshTokenQuery, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("logout")]
    [ExcludeFromPermission]
    public async Task<IActionResult> Logout([FromBody] LogOutQuery logOutQuery, CancellationToken cancellationToken)
    {
        await identityService.LogOutUser(logOutQuery, cancellationToken);

        return Ok(new { Message = "User logged out successfully." });
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    [ExcludeFromPermission]
    public async Task<ActionResult<ApiResponse<int>>> CreateArea(
    [FromBody] CreateUserCompanyCommand command, CancellationToken cancellationToken)
    {
        var response = await identityService.CreateUserByCompany(command, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("GetUserById")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status404NotFound)]
    [PermissionFilter("دریافت کاربر", "T9")]

    public async Task<ActionResult<ApiResponse<UserDto>>> GetUserById([FromQuery] GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var response = await identityService.GetUserById(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("GetRoles")]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<RoleDto>>), StatusCodes.Status200OK)]
    [ExcludeFromPermission]
    public async Task<ActionResult<ApiResponse<PagedResult<RoleDto>>>> GetRoles(CancellationToken cancellationToken)
    {
        var response = await identityService.GetRoles(cancellationToken);

        return StatusCode(response.StatusCode, response);
    }


}