using Capitan360.Application.Common;
using Capitan360.Application.Services.Identity.Dtos;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Application.Services.UserCompany.Commands.Create;
using Capitan360.Application.Services.UserCompany.Commands.Update;
using Capitan360.Application.Services.UserCompany.Queries.GetUserByCompany;
using Capitan360.Application.Services.UserCompany.Queries.GetUsersByCompany;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers
{
    [Route("api/[controller]/{companyId}/users/")]
    [ApiController]
    public class AdminController(IIdentityService identityService, IUserContext userContext) : ControllerBase
    {
        [HttpGet("GetAllUsers")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<UserDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<UserDto>>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<PagedResult<UserDto>>>> GetUsers([FromQuery] GetUsersByCompanyQuery query, CancellationToken cancellationToken,
         [FromRoute] int companyId = 0)
        {
            query.CompanyId = companyId;

            var users = await identityService.GetUsersByCompany(query, cancellationToken);

            return Ok(users);
        }

        [HttpGet("GetUserByIdAndCompanyId/{userId}")]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetUserByIdAndCompanyId([FromRoute] int companyId,
    [FromRoute] string userId, CancellationToken cancellationToken)
        {
            var query = new GetUserByCompanyQuery { CompanyId = companyId, UserId = userId };

            var response = await identityService.GetUserByCompany(query, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("users")]
        public IActionResult CreateUser([FromRoute] int companyId, CreateUserCompanyCommand createUserByCompanyCommand, CancellationToken cancellationToken)
        {
            createUserByCompanyCommand.CompanyId = companyId;
            var userId = identityService.CreateUserByCompany(createUserByCompanyCommand, cancellationToken);

            return CreatedAtAction(nameof(GetUserByIdAndCompanyId), new { companyId, userId }, null);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int companyId, [FromRoute] string userId, [FromBody] UpdateUserCompanyCommand updateUserCompanyCommand, CancellationToken cancellationToken)
        {
            updateUserCompanyCommand.UserId = userId;
            updateUserCompanyCommand.CompanyId = companyId;

            await identityService.UpdateUserCompany(updateUserCompanyCommand, cancellationToken);

            return NoContent();
        }
    }
}