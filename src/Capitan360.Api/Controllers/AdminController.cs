using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Common;
using Capitan360.Application.Features.Companies.UserCompany.Commands.Create;
using Capitan360.Application.Features.Companies.UserCompany.Commands.UpdateUserCompany;
using Capitan360.Application.Features.Companies.UserCompany.Queries.GetUserByCompany;
using Capitan360.Application.Features.Companies.UserCompany.Queries.GetUsersByCompany;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Application.Features.Identities.Users.Users.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers
{
    [Route("api/Admin/{companyId}/users/")]
    [ApiController]
    [PermissionFilter("مدیریت", "B")]
    public class AdminController(IIdentityService identityService, IUserContext userContext) : ControllerBase
    {
        [HttpGet("GetAllUsers")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<UserDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<UserDto>>), StatusCodes.Status400BadRequest)]
        [PermissionFilter("لیست کاربران", "B1")]
        public async Task<ActionResult<ApiResponse<PagedResult<UserDto>>>>
            GetUsers([FromQuery] GetUsersQuery query, CancellationToken cancellationToken,
         [FromRoute] int companyId = 0)
        {
            query.CompanyId = companyId;

            var users = await identityService.GetUsersByCompany(query, cancellationToken);

            return Ok(users);
        }

        [HttpGet("GetUserByIdAndCompanyId/{userId}")]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status400BadRequest)]

        [PermissionFilter("دریافت کاربر بر اساس شناسه کاربر و شناسه شرکت", "B2")]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetUserByIdAndCompanyId([FromRoute] int companyId,
    [FromRoute] string userId, CancellationToken cancellationToken)
        {
            var query = new GetUserByCompanyQuery { CompanyId = companyId, UserId = userId };

            var response = await identityService.GetUserByCompany(query, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("users")]
        [ExcludeFromPermission]
        public IActionResult CreateUser([FromRoute] int companyId, CreateUserCompanyCommand createUserByCompanyCommand, CancellationToken cancellationToken)
        {
            createUserByCompanyCommand.CompanyId = companyId;
            var userId = identityService.CreateUserByCompany(createUserByCompanyCommand, cancellationToken);

            return CreatedAtAction(nameof(GetUserByIdAndCompanyId), new { companyId, userId }, null);
        }

        [HttpPut("{userId}")]
        [ExcludeFromPermission]
        public async Task<IActionResult> UpdateUser([FromRoute] int companyId, [FromRoute] string userId, [FromBody] UpdateUserCompanyCommand updateUserCompanyCommand, CancellationToken cancellationToken)
        {
            updateUserCompanyCommand.UserId = userId;
            updateUserCompanyCommand.CompanyId = companyId;

            await identityService.UpdateUserCompany(updateUserCompanyCommand, cancellationToken);

            return NoContent();
        }
    }
}