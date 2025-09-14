using Microsoft.AspNetCore.Identity;

namespace Capitan360.Domain.Entities.Authorizations;

public class Role : IdentityRole
{
    public string? PersianName { get; set; }
    public bool Visible { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; } = [];
    public ICollection<RoleGroup> RoleGroups { get; set; } = [];
}