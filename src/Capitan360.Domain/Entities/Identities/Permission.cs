using Capitan360.Domain.Entities.BaseEntities;

namespace Capitan360.Domain.Entities.Identities;

public class Permission : BaseEntity
{
    public Guid PermissionCode { get; set; }

    public string Name { get; set; } = default!;

    public string DisplayName { get; set; } = default!;

    public bool Active { get; set; }

    public Guid ParentCode { get; set; }

    public string ParentName { get; set; } = default!;

    public string ParentDisplayName { get; set; } = default!;

    public ICollection<UserPermission> UserPermissions { get; set; } = [];
}
