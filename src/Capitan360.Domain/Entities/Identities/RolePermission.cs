using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.Identities;

public class RolePermission : BaseEntity
{
    public string RoleId { get; set; }
    public Role Role { get; set; }

    public int PermissionId { get; set; }
    public Permission Permission { get; set; }


}
