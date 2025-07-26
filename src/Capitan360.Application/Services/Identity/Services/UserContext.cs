namespace Capitan360.Application.Services.Identity.Services;

using Capitan360.Domain.Constants;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = (httpContextAccessor?.HttpContext?.User ?? null)
                   ?? throw new InvalidOperationException("User Context is not Present!");

        if (user.Identity is not { IsAuthenticated: true }) return null;

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        var companyId = user.FindFirstValue(ConstantNames.CompanyId) ?? "-1";
        var permissionVersionControl = user.Claims.FirstOrDefault(c => c.Type == ConstantNames.Pvc)?.Value ?? "";
        var mobile = user.FindFirstValue(ClaimTypes.MobilePhone) ?? "";
        var roles = user.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();
        var sessionId = user.Claims.FirstOrDefault(c => c.Type == ConstantNames.SessionId)?.Value ?? "";
        var companyType = user.Claims.FirstOrDefault(c => c.Type == ConstantNames.CompanyType)?.Value ?? "-1";
        var isParentCompany = user.Claims.FirstOrDefault(c => c.Type == ConstantNames.IsParentCompany)?.Value ?? "false";



        var permissionsClaim = user.Claims.FirstOrDefault(c => c.Type == ConstantNames.Permissions);
        var permissions = permissionsClaim!.Value.Split(',').ToList();
        var convertedCompanyId = int.Parse(companyId);
        var convertedCompanyType = int.Parse(companyType);
        var isParent = bool.Parse(isParentCompany);


        return new CurrentUser(userId!, mobile!, roles, permissions, convertedCompanyId, permissionVersionControl, sessionId, convertedCompanyType, isParent);
    }
}