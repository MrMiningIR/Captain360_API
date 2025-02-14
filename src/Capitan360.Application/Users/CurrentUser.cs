
namespace Capitan360.Application.Users;

public record CurrentUser(string Id, string Mobile, IEnumerable<string> Roles, IEnumerable<string> Permissions)
{
    public bool HasPermission(string permissionName)
    {
        return Permissions.Contains(permissionName);
    }
    public bool HasRole(string roleName)
    {
        return Roles.Contains( roleName);
    }
    public bool IsAdmin()
    {
        return HasRole("Admin");
    }
    public bool IsUser()
    {
        return HasRole("User");
    }
    public bool IsSuperAdmin()
    {
        return HasRole("SuperAdmin");
    }

}