using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.Identities;

public class Group: BaseEntity
{
   
    public string Name { get; set; }
    public ICollection<GroupPermission> GroupPermissions { get; set; } = [];
    public ICollection<RoleGroup> RoleGroups { get; set; } = [];
    public ICollection<UserGroup> UserGroups { get; set; } = [];


}
