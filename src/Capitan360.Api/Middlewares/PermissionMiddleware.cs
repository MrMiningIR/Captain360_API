using Capitan360.Domain.Entities.UserEntity;
using Capitan360.Infrastructure.Authorization.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Capitan360.Api.Middlewares;

public class PermissionMiddleware : IMiddleware
{
    private readonly UserManager<User> _userManager;
    private readonly IPermissionService _permissionService;

    public PermissionMiddleware(UserManager<User> userManager, IPermissionService permissionService)
    {
        _userManager = userManager;
        _permissionService = permissionService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var user = context.User;
        if (user.Identity is { IsAuthenticated: true })
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var sessionId = user.Claims.FirstOrDefault(c => c.Type == "SessionId")?.Value;

            // چک کردن Session
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(sessionId))
            {
                var dbUser = await _userManager.FindByIdAsync(userId);
                if (dbUser == null || dbUser.ActiveSessionId != sessionId)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Session expired or invalid.");
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("User ID or Session ID missing.");
                return;
            }
            // چک کردن گروه‌ها (اختیاری - اگه هنوز لازم داری)
            var groupIds = user.Claims.Where(c => c.Type == "GroupId").Select(c => int.Parse(c.Value)).ToList();
            if (!groupIds.Any())
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("User is not assigned to any group.");
                return;
            }
            // چک کردن مجوزها بر اساس Policyهای روت
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                var authorizeData = endpoint.Metadata.GetOrderedMetadata<IAuthorizeData>() ?? [];
                foreach (var data in authorizeData)
                {
                    if (!string.IsNullOrEmpty(data.Policy))
                    {
                        var hasPermission = await _permissionService.HasPermissionAsync(userId, data.Policy);
                        if (!hasPermission)
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            await context.Response.WriteAsync($"Permission '{data.Policy}' denied.");
                            return;
                        }
                    }
                }
            }

         
        }

        await next(context);
    }
}
//public class PermissionMiddleware(UserManager<User> userManager) : IMiddleware
//{
//    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
//    {
//        var user = context.User;
//        if (user.Identity is { IsAuthenticated: true })
//        {
//            var sessionId = user.Claims.FirstOrDefault(c => c.Type == "SessionId")?.Value;
//            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
//            var groupIds = user.Claims.Where(c => c.Type == "GroupId").Select(c => int.Parse(c.Value)).ToList();
//            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(sessionId))
//            {
//                var dbUser = await userManager.FindByIdAsync(userId);
//                if (dbUser == null || dbUser.ActiveSessionId != sessionId)
//                {
//                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
//                    await context.Response.WriteAsync("Session expired or invalid.");
//                    return;
//                }
//            }
//            if (!groupIds.Any())
//            {
//                context.Response.StatusCode = StatusCodes.Status403Forbidden;
//                await context.Response.WriteAsync("User is not assigned to any group.");
//                return;
//            }
//        }
//        await next(context);
//    }
//            // Must Get The route and the permission from the database and check if the user has the permission to access the route 
//            // This is just a dummy implementation

//            //  var requiredPermission = GetRequiredPermissionForRoute(context.Request.Path);
//            //  var permissions = user.Claims.Where(c => c.Type == "Permission").Select(c => c.Value).ToList();
//            //if (!string.IsNullOrEmpty(requiredPermission) && !permissions.Contains(requiredPermission))
//            //{
//            //    context.Response.StatusCode = StatusCodes.Status403Forbidden;
//            //    await context.Response.WriteAsync("Access Denied: You do not have the required permission.");
//            //    return;
//            //}

//    //private static string GetRequiredPermissionForRoute(PathString path)
//    //{
//    //    var routePermissions = new Dictionary<string, string>
//    //    {
//    //        { "/api/users", "ViewUsers" },
//    //        { "/api/users/edit", "EditUsers" }
//    //    };
//    //    return routePermissions.TryGetValue(path, out var permission) ? permission : null;
//    //}
//}
