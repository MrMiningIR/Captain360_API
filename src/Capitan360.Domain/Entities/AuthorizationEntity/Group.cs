using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.AuthorizationEntity
{
    public class Group: Entity
    {
       
        public string Name { get; set; }
        public ICollection<GroupPermission> GroupPermissions { get; set; } = [];
        public ICollection<RoleGroup> RoleGroups { get; set; } = [];
        public ICollection<UserGroup> UserGroups { get; set; } = [];


    }
}
