using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Entities.UserEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Capitan360.Application.Services.Identity;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Capitan360.Infrastructure.Constants;
using Capitan360.Application.Services.Identity.Users.Commands.CreateUser;
using Capitan360.Application.Services.Identity.Users.Queries.LoginUser;
using Capitan360.Application.Services.Identity.Users.Queries.LogOut;
using Capitan360.Application.Services.Identity.Users.Queries.RefreshToken;
using Capitan360.Application.Services.Identity.Users.Commands.AddUserGroup;
using Capitan360.Application.Services.Identity.Users.Queries.GetUserGroup;

namespace Capitan360.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentityController(UserManager<User> userManager,
    RoleManager<Role> roleManager,
  IConfiguration configuration,
    IIdentityService identityService) : ControllerBase

{
    // POST: api/identity/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserCommand createUserCommand, CancellationToken cancellationToken)
    {

        await identityService.RegisterUser(createUserCommand, StringValues.UserRole, cancellationToken);
        return Ok(new { Message = "User registered successfully." });
    }
    // POST: api/identity/login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserQuery loginUserQuery, CancellationToken cancellationToken)
    {

        var result = await identityService.LoginUser(loginUserQuery, cancellationToken);

        return Ok(result);

    }
    // POST: api/identity/login
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenQuery refreshTokenQuery, CancellationToken cancellationToken)
    {

        var result = await identityService.RefreshToken(refreshTokenQuery, cancellationToken);

        return Ok(result);

    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogOutQuery logOutQuery, CancellationToken cancellationToken)
    {
        await identityService.LogOutUser(logOutQuery, cancellationToken);

        return Ok(new { Message = "User logged out successfully." });
    }

    // GET: api/identity/users
    //[HttpGet("users")]
    //[Authorize(Roles = "Admin")]
    //public async Task<IActionResult> GetUsers()
    //{
    //    var users = await userManager.Users
    //        .Select(u => new
    //        {
    //            u.Id,
    //            u.Email,
    //            u.FullName,
    //            u.CapitanCargoCode,
    //            u.Active,
    //            Roles = userManager.GetRolesAsync(u).Result
    //        })
    //        .ToListAsync();

    //    return Ok(users);
    //}

    //// PUT: api/identity/users/{id}/activate
    //[HttpPut("users/{id}/activate")]
    //[Authorize(Roles = "Admin")]
    //public async Task<IActionResult> ActivateUser(string id)
    //{
    //    var user = await userManager.FindByIdAsync(id);
    //    if (user == null)
    //    {
    //        return NotFound(new { Message = "User not found." });
    //    }

    //    user.Active = true;
    //    await userManager.UpdateAsync(user);

    //    return Ok(new { Message = "User activated successfully." });
    //}

    //// DELETE: api/identity/users/{id}
    //[HttpDelete("users/{id}")]
    //[Authorize(Roles = "Admin")]
    //public async Task<IActionResult> DeleteUser(string id)
    //{
    //    var user = await userManager.FindByIdAsync(id);
    //    if (user == null)
    //    {
    //        return NotFound(new { Message = "User not found." });
    //    }

    //    var result = await userManager.DeleteAsync(user);
    //    if (!result.Succeeded)
    //    {
    //        return BadRequest(result.Errors);
    //    }

    //    return Ok(new { Message = "User deleted successfully." });
    //}

    //// POST: api/identity/roles
    //[HttpPost("roles")]
    //[Authorize(Roles = "Admin")]
    //public async Task<IActionResult> CreateRole([FromBody] string roleName)
    //{
    //    if (string.IsNullOrWhiteSpace(roleName))
    //    {
    //        return BadRequest(new { Message = "Role name is required." });
    //    }

    //    if (await roleManager.RoleExistsAsync(roleName))
    //    {
    //        return Conflict(new { Message = "Role already exists." });
    //    }

    //    var role = new Role { Name = roleName };
    //    var result = await roleManager.CreateAsync(role);

    //    if (!result.Succeeded)
    //    {
    //        return BadRequest(result.Errors);
    //    }

    //    return Ok(new { Message = "Role created successfully." });
    //}


    [HttpPost("addusertogroup")]
    public async Task<IActionResult> AddUserToGroup([FromBody] AddUserGroupCommand addUserGroupCommand, CancellationToken cancellationToken)
    {

        await identityService.AddUserToGroup(addUserGroupCommand, cancellationToken);

        return Ok(new { Message = "User added to the group successfully." });


    }
    [HttpPost("removeuserfromgroup")]
    public async Task<IActionResult> RemoveUserFromGroup([FromBody] GetUserGroupQuery getUserGroupQuery, CancellationToken cancellationToken)
    {
        await identityService.RemoveUserFromGroup(getUserGroupQuery, cancellationToken);

        return Ok(new { Message = "User removed from the group successfully." });
    }


}
