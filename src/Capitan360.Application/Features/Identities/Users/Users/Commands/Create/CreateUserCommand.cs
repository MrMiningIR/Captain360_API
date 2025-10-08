namespace Capitan360.Application.Features.Identities.Users.Users.Commands.Create;

public record CreateUserCommand(
    int CompanyId,
    int RoleId,
    string Password,
    string ConfirmPassword,
    string NameFamily,
    string AccountCodeInDesktopCaptainCargo,
    string Mobile,
    string MobileTelegram,
    string Tell,
    short TypeOfFactorInSamanehMoadianId,
    string Email,
    string NationalCode,
    string EconomicCode,
    string NationalId,
    string RegistrationId,
    short UserKindId,
    string Description);
