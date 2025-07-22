using Capitan360.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Domain.Entities.UserEntity;

public class UserPermissionVersionControl : Entity
{
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = default!;
    public User? User { get; set; }


    public string PermissionVersion { get; set; } = default!;
}