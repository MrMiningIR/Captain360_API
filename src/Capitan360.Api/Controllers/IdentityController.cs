using Capitan360.Domain.Entities.AuthorizationEntity;
using Capitan360.Domain.Entities.UserEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Capitan360.Application.Services.Identity;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Capitan360.Application.Services.Identity.Users;
using Capitan360.Infrastructure.Constants;

using Capitan360.Application.Services.Identity.Users.Commands;
using Capitan360.Application.Services.Identity.Users.Queries;

namespace Capitan360.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentityController(UserManager<User> userManager,
    RoleManager<Role> roleManager,
  IConfiguration configuration,
    ApplicationDbContext context, IIdentityService identityService) : ControllerBase

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
    public async Task<IActionResult> Login(LoginUserQuery loginUserQuery ,CancellationToken cancellationToken)
    {

      var result =  await identityService.LoginUser(loginUserQuery, cancellationToken);

      return Ok(result);
       
    }
    // POST: api/identity/login
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenQuery refreshTokenQuery ,CancellationToken cancellationToken)
    {

      var result =  await identityService.RefreshToken(refreshTokenQuery, cancellationToken);

      return Ok(result);
       
    }

    // GET: api/identity/users
    [HttpGet("users")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await userManager.Users
            .Select(u => new
            {
                u.Id,
                u.Email,
                u.FullName,
                u.CapitanCargoCode,
                u.Active,
                Roles = userManager.GetRolesAsync(u).Result
            })
            .ToListAsync();

        return Ok(users);
    }

    // PUT: api/identity/users/{id}/activate
    [HttpPut("users/{id}/activate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ActivateUser(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound(new { Message = "User not found." });
        }

        user.Active = true;
        await userManager.UpdateAsync(user);

        return Ok(new { Message = "User activated successfully." });
    }

    // DELETE: api/identity/users/{id}
    [HttpDelete("users/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound(new { Message = "User not found." });
        }

        var result = await userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new { Message = "User deleted successfully." });
    }

    // POST: api/identity/roles
    [HttpPost("roles")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateRole([FromBody] string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
        {
            return BadRequest(new { Message = "Role name is required." });
        }

        if (await roleManager.RoleExistsAsync(roleName))
        {
            return Conflict(new { Message = "Role already exists." });
        }

        var role = new Role { Name = roleName };
        var result = await roleManager.CreateAsync(role);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new { Message = "Role created successfully." });
    }
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutModel model)
    {
        // Find the user by UserId
        var user = await userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return NotFound(new { Message = "User not found." });
        }

        // Check if the provided SessionId matches the ActiveSessionId
        if (user.ActiveSessionId != model.SessionId)
        {
            return Unauthorized(new { Message = "Invalid session." });
        }

        // Clear ActiveSessionId
        user.ActiveSessionId = null;
        await userManager.UpdateAsync(user);

        // Add the token to the blacklist
        var tokenBlacklist = new TokenBlacklist
        {
            Token = model.Token,
            ExpiryDate = DateTime.UtcNow.AddHours(24), // Set expiration based on token lifetime
            UserId = model.UserId
        };

        context.TokenBlacklists.Add(tokenBlacklist);
        await context.SaveChangesAsync();

        return Ok(new { Message = "User logged out successfully." });
    }

    [HttpPost("add-user-to-group")]
    public async Task<IActionResult> AddUserToGroup([FromBody] UserGroupModel model)
    {
        var user = await userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return NotFound(new { Message = "User not found." });
        }

        var group = await context.Groups.FindAsync(model.GroupId);
        if (group == null)
        {
            return NotFound(new { Message = "Group not found." });
        }

        // Check if the user is already in the group
        var existingUserGroup = await context.UserGroups
            .FirstOrDefaultAsync(ug => ug.UserId == model.UserId && ug.GroupId == model.GroupId);

        if (existingUserGroup != null)
        {
            return Conflict(new { Message = "User is already in the group." });
        }

        // Add user to the group
        var userGroup = new UserGroup
        {
            UserId = model.UserId,
            GroupId = model.GroupId
        };

        context.UserGroups.Add(userGroup);
        await context.SaveChangesAsync();

        return Ok(new { Message = "User added to the group successfully." });
    }
    [HttpPost("remove-user-from-group")]
    public async Task<IActionResult> RemoveUserFromGroup([FromBody] UserGroupModel model)
    {
        var user = await userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return NotFound(new { Message = "User not found." });
        }

        var group = await context.Groups.FindAsync(model.GroupId);
        if (group == null)
        {
            return NotFound(new { Message = "Group not found." });
        }

        // Find the user-group relationship
        var userGroup = await context.UserGroups
            .FirstOrDefaultAsync(ug => ug.UserId == model.UserId && ug.GroupId == model.GroupId);

        if (userGroup == null)
        {
            return NotFound(new { Message = "User is not in the group." });
        }

        // Remove user from the group
        context.UserGroups.Remove(userGroup);
        await context.SaveChangesAsync();

        return Ok(new { Message = "User removed from the group successfully." });
    }
}
