using Microsoft.AspNetCore.Identity;

namespace Capitan360.Domain.Entities.AuthorizationEntity;

public class Role : IdentityRole
{
    public ICollection<RolePermission> RolePermissions { get; set; } = [];
    public ICollection<RoleGroup> RoleGroups { get; set; } = [];
}