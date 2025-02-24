using Capitan360.Application.Services.Identity.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capitan360.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController(IUserContext userContext) : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "CreateRoles")]
        public IActionResult Get()
        {
            var user = userContext.GetCurrentUser();
         
            return Ok("Hello World");
        }
       

    }
}
