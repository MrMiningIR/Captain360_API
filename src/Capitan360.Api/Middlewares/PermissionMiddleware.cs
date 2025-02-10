using Capitan360.Domain.Entities.UserEntity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Capitan360.Api.Middlewares;
public class PermissionMiddleware(UserManager<User> userManager) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var user = context.User;
        if (user.Identity is { IsAuthenticated: true })
        {
            var sessionId = user.Claims.FirstOrDefault(c => c.Type == "SessionId")?.Value;
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(sessionId))
            {
                var dbUser = await userManager.FindByIdAsync(userId);
                if (dbUser == null || dbUser.ActiveSessionId != sessionId)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Session expired or invalid.");
                    return;
                }
            }
            var requiredPermission = GetRequiredPermissionForRoute(context.Request.Path);
            var permissions = user.Claims.Where(c => c.Type == "Permission").Select(c => c.Value).ToList();
            if (!string.IsNullOrEmpty(requiredPermission) && !permissions.Contains(requiredPermission))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access Denied: You do not have the required permission.");
                return;
            }
        }
        await next(context);
    }

    private static string GetRequiredPermissionForRoute(PathString path)
    {
        var routePermissions = new Dictionary<string, string>
        {
            { "/api/users", "ViewUsers" },
            { "/api/users/edit", "EditUsers" }
        };
        return routePermissions.TryGetValue(path, out var permission) ? permission : null;
    }
}
