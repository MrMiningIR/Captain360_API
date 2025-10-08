using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.Identities.Users.Users.Dtos;

public class UserDto
{
    public int CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public int CompanyTypeId { get; set; }
    public int RoleId { get; set; }
    public string? RoleName { get; set; }
    public string NameFamily { get; set; } = default!;
    public string AccountCodeInDesktopCaptainCargo { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
    public string Mobile { get; set; } = default!;
    public string MobileTelegram { get; set; } = default!;
    public short TypeOfFactorInSamanehMoadianId { get; set; }
    public MoadianFactorType? TypeOfFactorInSamanehMoadian { get; set; }
    public string Tell { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string NationalCode { get; set; } = default!;
    public string EconomicCode { get; set; } = default!;
    public string NationalId { get; set; } = default!;
    public string RegistrationId { get; set; } = default!;
    public string Description { get; set; } = default!;
    public long Credit { get; set; }
    public bool HasCredit { get; set; }
    public bool Active { get; set; }
    public bool Baned { get; set; }
    public bool IsBikeDelivery { get; set; }
    public DateTime LastAccess { get; set; }
    public string ActiveSessionId { get; set; } = default!;
    public string PermissionVersion { get; set; } = default!;
    public string ActivationCode { get; set; } = default!;
    public DateTime ActivationCodeExpireTime { get; set; }
    public string RecoveryPasswordCode { get; set; } = default!;
    public DateTime RecoveryPasswordCodeExpireTime { get; set; }
}