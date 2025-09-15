using System.Security.Claims;
using Capitan360.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Capitan360.Api.Middlewares
{
    public class SessionValidationMiddleware(RequestDelegate next, UserManager<User> userManager)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var sessionId = context.User.Claims.FirstOrDefault(c => c.Type == "SessionId")?.Value;
            var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(sessionId))
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user == null || user.ActiveSessionId != sessionId)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Session expired or invalid.");
                    return;
                }
            }

            await next(context);
        }
    }
}
