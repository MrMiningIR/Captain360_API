using Microsoft.AspNetCore.Identity;

namespace Capitan360.Domain.Entities.Identities;

public class Role : IdentityRole
{
    public string PersianName { get; set; } = default!;

    public bool Visible { get; set; }
}