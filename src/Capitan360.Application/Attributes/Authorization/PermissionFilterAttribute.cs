using Capitan360.Application.Features.Identities.Identities.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Capitan360.Application.Attributes.Authorization;

public class PermissionFilterAttribute : ActionFilterAttribute
{
    public string? DisplayName { get; }
    public bool AllowAll { get; } = false;
    public string? PermissionCode { get; }

    public PermissionFilterAttribute(string? displayName, string permissionCode, bool allowAll = false)
    {
        AllowAll = allowAll;

        if (!allowAll && string.IsNullOrWhiteSpace(displayName))
        {
            throw new ArgumentException("DisplayName cannot be null or empty when AllowAll is false.", nameof(displayName));
        }
        if (!allowAll && string.IsNullOrWhiteSpace(permissionCode))
        {
            throw new ArgumentException("PermissionCode cannot be null or empty when AllowAll is false.", nameof(permissionCode));
        }

        DisplayName = displayName;
        PermissionCode = permissionCode;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var logger = context.HttpContext.RequestServices.GetService<ILogger<PermissionFilterAttribute>>();
        var userContext = context.HttpContext.RequestServices.GetService<IUserContext>();
        var currentUser = userContext?.GetCurrentUser();

        var controllerName = context.ActionDescriptor.RouteValues["controller"];
        var actionName = context.ActionDescriptor.RouteValues["action"];


        bool hasExcludeFromPermission = context.ActionDescriptor.EndpointMetadata
            .Any(em => em is ExcludeFromPermissionAttribute);
        bool hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata
            .Any(em => em is AllowAnonymousAttribute);

        if (AllowAll || hasAllowAnonymous || hasExcludeFromPermission)
        {
            logger?.LogInformation($"AllowAll or AllowAnonymous detected for {controllerName}.{actionName}. Skipping permission check.");
            return;
        }

        if (currentUser == null)
        {
            context.Result = new UnauthorizedResult();
            logger?.LogInformation("User is not authenticated");
            return;
        }

        if (currentUser.IsSuperAdmin())
        {
            logger?.LogInformation($"SuperAdmin detected for {controllerName}.{actionName}. Skipping permission check.");
            return;
        }



        string? permissionCodeToCheck = GetPermissionCode(context);

        if (string.IsNullOrWhiteSpace(permissionCodeToCheck))
        {
            context.Result = new UnauthorizedResult();
            logger?.LogInformation($"No valid PermissionFilter found for {controllerName}.{actionName}.");
            return;
        }
        bool hasPermission = currentUser.HasPermission(permissionCodeToCheck!);
        if (!hasPermission)
        {
            context.Result = new UnauthorizedResult();
            logger?.LogInformation($"User {currentUser.Id} does not have permission for {controllerName}.{actionName}.{permissionCodeToCheck}");
            return;
        }

        logger?.LogInformation($"User {currentUser.Id} has Granted!");

        base.OnActionExecuting(context);
    }
    private string? GetPermissionCode(ActionExecutingContext context)
    {

        if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
        {
            var methodPermission = controllerActionDescriptor.MethodInfo
                .GetCustomAttributes<PermissionFilterAttribute>()
                .FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.PermissionCode))
                ?.PermissionCode;

            if (!string.IsNullOrWhiteSpace(methodPermission))
            {
                return methodPermission;
            }


            var controllerPermission = controllerActionDescriptor.ControllerTypeInfo
                .GetCustomAttributes<PermissionFilterAttribute>()
                .FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.PermissionCode))
                ?.PermissionCode;

            return controllerPermission;
        }


        return context.ActionDescriptor.EndpointMetadata
            .OfType<PermissionFilterAttribute>()
            .FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.PermissionCode))
            ?.PermissionCode;
    }
}