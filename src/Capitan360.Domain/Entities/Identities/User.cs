using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Entities.Companies;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Capitan360.Domain.Entities.Identities;

public class User : IdentityUser
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }
    public int CompanyTypeId { get; set; }

    public string NameFamily { get; set; } = default!;

    public string AccountCodeInDesktopCaptainCargo { get; set; } = default!;

    public string? MobileTelegram { get; set; }

    public short TypeOfFactorInSamanehMoadianId { get; set; }

    public string? Tell { get; set; }

    public string? NationalCode { get; set; }

    public string? EconomicCode { get; set; }

    public string? NationalId { get; set; }

    public string? RegistrationId { get; set; }

    public string? Description { get; set; }

    public long Credit { get; set; }

    public bool HasCredit { get; set; }

    public bool Active { get; set; }

    public bool Baned { get; set; }

    public string? ActivationCode { get; set; }

    public DateTime ActivationCodeExpireTime { get; set; }

    public string? RecoveryPasswordCode { get; set; }
    public DateTime RecoveryPasswordCodeExpireTime { get; set; }

    public bool IsBikeDelivery { get; set; }

    public DateTime LastAccess { get; set; }

    public string? ActiveSessionId { get; set; }

    public string PermissionVersion { get; set; } = default!;

    public ICollection<Address> Addresses { get; set; } = [];

    public ICollection<Role> Roles { get; set; } = [];

    public ICollection<UserPermission> UserPermissions { get; set; } = [];

    [Timestamp]
    [JsonIgnore]
    public byte[] ConcurrencyToken { get; set; } = [0];
}