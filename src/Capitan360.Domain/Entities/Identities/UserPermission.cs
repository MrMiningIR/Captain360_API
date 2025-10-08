using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.BaseEntities;

namespace Capitan360.Domain.Entities.Identities;

public class UserPermission : BaseEntity
{
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = default!;
    public User? User { get; set; }

    [ForeignKey(nameof(Permission))]
    public int PermissionId { get; set; }
    public Permission? Permission { get; set; }
}