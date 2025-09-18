using Capitan360.Application.Attributes.Authorization;
using Capitan360.Application.Features.Permission.Dtos;
using Capitan360.Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Capitan360.Application.Features.Permission.Services;

public class PermissionCollectorService(ILogger<PermissionCollectorService> logger)
{
    public List<PermissionCollectorDto> GetActionsWithPermissionFilter(Assembly apiAssembly)
    {
        var result = new List<PermissionCollectorDto>();

        var controllers = apiAssembly.GetTypes()
            .Where(type => typeof(ControllerBase).IsAssignableFrom(type) && !type.IsAbstract);

        foreach (var controller in controllers)
        {
            var controllerFilter = controller.GetCustomAttribute<PermissionFilterAttribute>();
            var controllerName = controller.Name.Replace("Controller", "");
            var controllerDisplayName = controllerFilter?.DisplayName ?? controllerName; // نام نمایشی کنترلر

            // فقط کنترلرهایی که PermissionFilter دارند بررسی شوند
            if (controllerFilter == null || controllerFilter.AllowAll)
            {
                continue;
            }

            var actions = controller.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(m => !m.IsDefined(typeof(NonActionAttribute)) &&
                            m.GetCustomAttributes().Any(attr => attr is HttpMethodAttribute) &&
                            !m.IsDefined(typeof(ExcludeFromPermissionAttribute)) &&
                            !m.IsDefined(typeof(AllowAnonymousAttribute)));

            foreach (var action in actions)
            {
                var actionFilter = action.GetCustomAttribute<PermissionFilterAttribute>();
                if (actionFilter == null || actionFilter.AllowAll)
                {
                    continue;
                }
                var displayName = actionFilter.DisplayName;
                var permissionName = actionFilter.PermissionCode;

                var dto = new PermissionCollectorDto(
                    DisplayName: displayName!,
                    PermissionName: permissionName!,
                    PermissionCode: Tools.GenerateDeterministicGuid(permissionName!),
                    Parent: controllerName,
                    ParentCode: Tools.GenerateDeterministicGuid(controller.Name),
                    SourceDisplayName: controllerDisplayName

                );
                //logger.LogInformation("Found permission: {Dto}", dto);
                result.Add(dto);
            }
        }

        return result;
    }
}

