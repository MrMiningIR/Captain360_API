using System.Security.Claims;
using Capitan360.Infrastructure.Authorization.Services;
using Microsoft.AspNetCore.Authorization;

namespace Capitan360.Infrastructure.Authorization.Requirements
{
    public class PermissionAuthorizationHandler(IPermissionService permissionService)
        : AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                context.Fail();
                return;
            }

            var hasPermission = await permissionService.HasPermissionAsync(userId, requirement.Permission);
            if (hasPermission)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}

//private readonly IServiceProvider _serviceProvider;

//public PermissionAuthorizationHandler(IServiceProvider serviceProvider)
//{
//    _serviceProvider = serviceProvider;
//}

//protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
//{
//    var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
//    if (userId == null)
//    {
//        context.Fail();
//        return;
//    }

//    // Create a scope for scoped services
//    using (var scope = _serviceProvider.CreateScope())
//    {
//        var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
//        var hasPermission = await permissionService.HasPermissionAsync(userId, requirement.Permission);
//        if (hasPermission)
//        {
//            context.Succeed(requirement);
//        }
//        else
//        {
//            context.Fail();
//        }
//    }
//}
