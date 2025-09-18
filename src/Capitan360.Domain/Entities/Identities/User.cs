using Capitan360.Domain.Entities.Companies;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Capitan360.Domain.Entities.Identities;

public class User : IdentityUser
{
    public string? FullName { get; set; }
    public string? CapitanCargoCode { get; set; }
    public bool Active { get; set; }
    public DateTime LastAccess { get; set; }
    public string? ActivationCode { get; set; }
    public DateTime ActivationCodeExpireTime { get; set; }

    public string? ActiveSessionId { get; set; }
    public int UserKind { get; set; }
    public int CompanyType { get; set; }



    // Navigation Properties
    public UserProfile? Profile { get; set; }

    public ICollection<UserGroup> UserGroups { get; set; } = [];
    public ICollection<UserCompany> UserCompanies { get; set; } = [];

    public ICollection<UserPermission> UserPermissions { get; set; } = [];
    public UserPermissionVersionControl? UserPermissionVersionControl { get; set; }

    // Navigation Property برای نقش‌ها
    public ICollection<Role> Roles { get; set; } = [];

    [JsonIgnore]
    public byte[] ConcurrencyToken { get; set; } = [0];


}