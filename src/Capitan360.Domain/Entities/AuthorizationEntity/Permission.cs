using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.AuthorizationEntity;

public class Permission: Entity
{
    
    public string Name { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; } = [];
    public ICollection<GroupPermission> GroupPermissions { get; set; } = [];
}
