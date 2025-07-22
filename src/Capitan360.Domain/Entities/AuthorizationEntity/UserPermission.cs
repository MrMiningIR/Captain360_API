using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.UserEntity;

namespace Capitan360.Domain.Entities.AuthorizationEntity;

public class UserPermission : Entity
{
    public string UserId { get; set; }
    public User User { get; set; }

    public int PermissionId { get; set; }
    public Permission Permission { get; set; }
}