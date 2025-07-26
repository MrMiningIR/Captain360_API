using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.Identity.Services;

public record CurrentUser(
    string Id,
    string Mobile,
    List<string> Roles,
    List<string> Permissions,
    int CompanyId,
    string PermissionVersionControl,
    string SessionId,
    int CompanyType,
    bool IsParent)
{
    public bool HasPermission(string permissionName)
    {
        return Permissions.Contains(permissionName);
    }
    public bool HasRole(string roleName)
    {
        return Roles.Contains(roleName);
    }
    public bool IsManager()
    {
        return HasRole(ConstantNames.ManagerRole);
    }
    public bool IsUser()
    {
        return HasRole(ConstantNames.UserRole);
    }
    public bool IsSuperAdmin()
    {
        return HasRole(ConstantNames.SuperAdminRole);
    }
    public bool IsAdministratorGroup()
    {
        return HasRole(ConstantNames.SuperAdminRole) || HasRole(ConstantNames.ManagerRole);
    }
    public bool IsParentCompany()
    {
        return IsParent;
    }

    public int GetCompanyId()
    {
        return CompanyId;
    }
    public int GetCompanyType()
    {
        return CompanyType;
    }
    public string GetPermissionVersionControl()
    {
        return PermissionVersionControl;
    }
    public string GetSessionId()
    {
        return SessionId;
    }

    public bool ValidateCompanyId()
    {
        if (CompanyId <= 0 && !IsSuperAdmin())
            return false;

        return true;
    }
}