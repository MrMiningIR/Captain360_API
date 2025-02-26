using Capitan360.Application.Services.Identity;
using Capitan360.Application.Services.UserCompany;
using Capitan360.Application.Services.UserCompany.Commands.Create;
using Capitan360.Application.Services.UserCompany.Commands.Update;
using Capitan360.Application.Services.UserCompany.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers
{
    [Route("api/[controller]/{companyId}/users")]
    [ApiController]
    public class AdminController(IIdentityService identityService) : ControllerBase
    {

       
        [HttpGet("users")]
        public async Task<ActionResult<IReadOnlyList<UserDto>>> GetUsers([FromRoute] int companyId, CancellationToken cancellationToken)
        {

            var getUsersByCompanyQuery = new GetUsersByCompanyQuery(companyId);

            var users = await identityService.GetUsersByCompany(getUsersByCompanyQuery, cancellationToken);

            return Ok(users);
        }
      
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> GetUserById([FromRoute] int companyId, [FromRoute] string userId, CancellationToken cancellationToken)
        {
            var getUsersByCompanyQuery = new GetUserByCompanyQuery(userId, companyId);
            var user = await identityService.GetUserByCompany(getUsersByCompanyQuery, cancellationToken);

            return Ok(user);
        }
      
        [HttpPost("users")]
        public IActionResult CreateUser([FromRoute] int companyId , CreateUserCompanyCommand createUserByCompanyCommand, CancellationToken cancellationToken)
        {
            createUserByCompanyCommand.CompanyId = companyId;
            var userId = identityService.CreateUserByCompany(createUserByCompanyCommand, cancellationToken);

            
            return CreatedAtAction(nameof(GetUserById), new { companyId, userId }, null);
        }
       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int companyId, [FromRoute] string userId ,UpdateUserCompanyCommand updateUserCompanyCommand ,CancellationToken cancellationToken)
        {

            updateUserCompanyCommand.UserId = userId;
            updateUserCompanyCommand.CompanyId = companyId;

            await identityService.UpdateUserCompany(updateUserCompanyCommand, cancellationToken);


            return NoContent();
        }
     
        //[HttpDelete("{id}")]
        //public IActionResult DeleteUser(int id)
        //{
        //    return Ok();
        //}

    }




}
