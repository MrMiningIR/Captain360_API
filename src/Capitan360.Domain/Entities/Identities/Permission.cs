using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.Identities;

public class Permission : BaseEntity
{

    public string Name { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string Parent { get; set; }
    public string ParentDisplayName { get; set; }
    public Guid PermissionCode { get; set; }
    public Guid ParentCode { get; set; }
    public bool Active { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; } = [];
    public ICollection<GroupPermission> GroupPermissions { get; set; } = [];
    public ICollection<UserPermission> UserPermissions { get; set; } = [];
}
