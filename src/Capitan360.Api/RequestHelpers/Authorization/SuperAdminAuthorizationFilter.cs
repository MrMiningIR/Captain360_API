using Capitan360.Application.Features.Identities.Identities.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Capitan360.Api.RequestHelpers.Authorization;

public class CompanyIdAuthorizationFilter(IUserContext userContext, ILogger<CompanyIdAuthorizationFilter> logger) : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var currentUser = userContext.GetCurrentUser();

        if (currentUser == null || !currentUser.IsAdministratorGroup())
        {
            context.Result = new UnauthorizedResult();
            logger.LogInformation("User is not SuperAdmin or not authorized");
            return;
        }

        if (currentUser.CompanyId == 0 && !currentUser.IsSuperAdmin())
        {
            context.Result = new UnauthorizedResult();
            logger.LogInformation("User is not SuperAdmin");
            return;
        }
        logger.LogInformation("User Has Accessibility as SuperAdmin");
    }
}