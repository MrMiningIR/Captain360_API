using Capitan360.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Domain.Entities.Users;

public class UserPermissionVersionControl : BaseEntity
{
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = default!;
    public User? User { get; set; }


    public string PermissionVersion { get; set; } = default!;
}