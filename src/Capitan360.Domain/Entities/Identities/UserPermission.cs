using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.Users;

namespace Capitan360.Domain.Entities.Identities;

public class UserPermission : BaseEntity
{
    public string UserId { get; set; }
    public User User { get; set; }

    public int PermissionId { get; set; }
    public Permission Permission { get; set; }
}