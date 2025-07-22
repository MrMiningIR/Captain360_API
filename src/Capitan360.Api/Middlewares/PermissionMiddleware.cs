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

            // Getting Permissions from (Role, Group, UserPermission)
            // var userPermissions = await permissionService.GetUserPermissionsAsync(userId);

            // Checking Groups
            //var groupIds = user.Claims.Where(c => c.Type == "GroupId").Select(c => int.Parse(c.Value)).ToList();
            //if (!groupIds.Any() && !userPermissions.Any())
            //if (!userPermissions.Any())

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
                #region Comment

                //var authorizeData = endpoint.Metadata.GetOrderedMetadata<IAuthorizeData>() ?? [];
                //foreach (var data in authorizeData)
                //{
                //    if (!string.IsNullOrEmpty(data.Policy))
                //    {
                //        //var hasPermission = permissionService.HasPermissionAsync(data.Policy, userPermissions);
                //        var hasPermission = await permissionService.HasPermissionAsync(userId, data.Policy);
                //        if (!hasPermission)
                //        {
                //            throw new ForbiddenException($"Permission '{data.Policy}' denied.");

                //        }
                //    }
                //}

                #endregion Comment

                var authorizeData = endpoint.Metadata.GetOrderedMetadata<PermissionFilterAttribute>() ?? [];
                var hasAllowAnonymous = endpoint.Metadata.Any(em => em is AllowAnonymousAttribute);
                var hasExcludeFromPermission = endpoint.Metadata.Any(em => em is ExcludeFromPermissionAttribute);

                if (hasExcludeFromPermission || hasAllowAnonymous || authorizeData.Any(data => data.AllowAll) || currentUser.IsSuperAdmin())
                {
                    await next(context);
                    return;
                }
                var controllerName = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>()?.ControllerName;
                var actionName = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>()?.ActionName;

                foreach (var data in authorizeData)
                {
                    if (!string.IsNullOrEmpty(actionName))
                    {
                        hasPermission = await permissionService.HasPermissionAsync(currentUser.Id, actionName);
                    }

                    if (!string.IsNullOrEmpty(controllerName))
                    {
                        hasControllerPermission = await permissionService.HasPermissionAsync(currentUser.Id, controllerName);
                    }

                    if (!hasPermission)
                    {
                        if (!hasControllerPermission)
                        {

                            throw new ForbiddenException($"Permission '{data.DisplayName}' denied.");
                        }
                    }
                    //if (!hasPermission || !hasControllerPermission)
                    //{
                    //    throw new ForbiddenException($"Permission '{data.DisplayName}' denied.");
                    //}

                }
            }
        }

        await next(context);
    }
}