namespace Capitan360.Application.Users;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{


    public CurrentUser? GetCurrentUser()
    {

        var user = (httpContextAccessor?.HttpContext?.User ?? null)
                   ?? throw new InvalidOperationException("User Context is not Present!");


       if (user.Identity is not { IsAuthenticated: true }) return null;
        
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        var mobile = user.FindFirstValue(ClaimTypes.MobilePhone);
        var roles = user.FindAll(ClaimTypes.Role).Select(x => x.Value);
        var permissions = user.FindAll("Permission").Select(x => x.Value);

        return new CurrentUser(userId!, mobile!, roles, permissions);




    }



}