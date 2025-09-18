using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.BaseEntities;

namespace Capitan360.Domain.Entities.Identities;

public class UserPermissionVersionControl : BaseEntity
{
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = default!;
    public User? User { get; set; }


    public string PermissionVersion { get; set; } = default!;
}