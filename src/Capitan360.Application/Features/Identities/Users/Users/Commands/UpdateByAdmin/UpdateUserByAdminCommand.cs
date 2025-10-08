namespace Capitan360.Application.Features.Identities.Users.Users.Commands.UpdateByAdmin;

public record UpdateUserByAdminCommand(
    string NameFamily,
    string AccountCodeInDesktopCaptainCargo,
    string MobileTelegram,
    string Tell,
    short TypeOfFactorInSamanehMoadianId,
    string Email,
    string NationalCode,
    string EconomicCode,
    string NationalId,
    string RegistrationId,
    bool Active,
    bool Baned,
    bool IsBikeDelivery,
    string Description)
{
    public string Id { get; set; }
};
