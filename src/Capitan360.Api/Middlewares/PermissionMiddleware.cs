using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.UserEntity;
using Capitan360.Domain.Exceptions;
using Capitan360.Domain.Repositories.PermissionRepository;
using Capitan360.Infrastructure.Authorization.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace Capitan360.Api.Middlewares;

public class PermissionMiddleware(UserManager<User> userManager,
    IPermissionService permissionService,
    IUserPermissionVersionControlRepository permissionVersionControlRepository
    , IUserContext userContext)
    : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var cancellationToken = context.RequestAborted;
        var currentUser = userContext.GetCurrentUser();

        if (currentUser is not null)
        {
            bool hasPermission = false;
            bool hasControllerPermission = false;

            if (string.IsNullOrWhiteSpace(currentUser.GetPermissionVersionControl()))
                throw new ForbiddenForceLogoutException(ConstantNames.UserNotValidMessage);

            if (string.IsNullOrEmpty(currentUser.Id) || string.IsNullOrEmpty(currentUser.GetSessionId()))
                throw new ForbiddenForceLogoutException(ConstantNames.UserNotValidMessage);

            var dbUser = await userManager.FindByIdAsync(currentUser.Id);

            //if (!string.IsNullOrEmpty(dbUser!.ActiveSessionId))
            //{
            //    if (dbUser.ActiveSessionId != currentUser.GetSessionId())
            //    {
            //        dbUser.ActiveSessionId = null;
            //        await userManager.UpdateAsync(dbUser);
            //        throw new ForbiddenForceLogoutException(ConstantNames.UserAlreadyLoggined);

            //    }
            //}

            if (!dbUser!.Active)
                throw new ForbiddenForceLogoutException(ConstantNames.DeactivatedAccountMessage);

            var savedUserPvc = await permissionVersionControlRepository.GetUserPermissionVersionString(currentUser.Id, cancellationToken);

            if (string.IsNullOrEmpty(savedUserPvc))
                throw new ForbiddenForceLogoutException(ConstantNames.UserNotValidMessage);

            if (savedUserPvc != currentUser.GetPermissionVersionControl())
            {
                //  identityService.LogOutUser(new LogOutQuery(userId,sessionId,),cancellationToken);

                throw new ForbiddenForceLogoutException(ConstantNames.ChangedAccessMessage);
            }



            // Check CompanyId

            if ((userContext.GetCurrentUser()!.IsUser() && (string.IsNullOrEmpty(currentUser.CompanyId.ToString()) || currentUser.CompanyId <= 0)))
            {
                throw new ForbiddenForceLogoutException(ConstantNames.UserNotValidMessage);
            }

            // Check Permission
            if (!await permissionService.HasAnyPermissionAsync(currentUser.Id) && currentUser.IsUser())
            {
                throw new ForbiddenForceLogoutException(ConstantNames.UserHasNoAccessMessage);
            }

            // Checking Permissions for each endpoint

            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                // var authorizeData = endpoint.Metadata.GetOrderedMetadata<PermissionFilterAttribute>() ?? [];
                var hasAllowAnonymous = endpoint.Metadata.Any(em => em is AllowAnonymousAttribute);
                var hasExcludeFromPermission = endpoint.Metadata.Any(em => em is ExcludeFromPermissionAttribute);


                if (hasAllowAnonymous || hasExcludeFromPermission || currentUser.IsSuperAdmin())
                {
                    // _logger?.LogInformation("AllowAnonymous or ExcludeFromPermission detected. Skipping permission check.");
                    await next(context);
                    return;
                }

                var action = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
                var actionPermissionFilter = action!.MethodInfo.GetCustomAttribute<PermissionFilterAttribute>();
                if (actionPermissionFilter != null && !actionPermissionFilter.AllowAll)
                {
                    hasPermission = await permissionService.HasPermissionAsync(currentUser.Id, actionPermissionFilter!.PermissionCode!);
                    if (!hasPermission)
                    {
                        throw new ForbiddenException($"Permission '{actionPermissionFilter.DisplayName}-{action.ActionName}' denied.");
                    }
                }
            }
        }

        await next(context);
    }
}

