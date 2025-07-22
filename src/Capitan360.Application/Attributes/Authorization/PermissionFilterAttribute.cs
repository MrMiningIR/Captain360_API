using Capitan360.Application.Services.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Attributes.Authorization;

public class PermissionFilterAttribute : ActionFilterAttribute
{
    public string? DisplayName { get; }
    public bool AllowAll { get; } = false;

    public PermissionFilterAttribute(string? displayName, bool allowAll = false)
    {
        AllowAll = allowAll;

        if (!allowAll && string.IsNullOrWhiteSpace(displayName))
        {
            throw new ArgumentException("DisplayName cannot be null or empty when AllowAll is false.", nameof(displayName));
        }

        DisplayName = displayName;
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


        bool hasPermission = currentUser.HasPermission(actionName!) || currentUser.HasPermission(controllerName!);
        if (!hasPermission)
        {
            context.Result = new UnauthorizedResult();
            logger?.LogInformation($"User {currentUser.Id} does not have permission for {controllerName}.{actionName}");
            return;
        }

        logger?.LogInformation($"User {currentUser.Id} has Granted!");

        base.OnActionExecuting(context);
    }
}