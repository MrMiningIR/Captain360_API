namespace Capitan360.Application.Features.Identities.Users.Users.Commands.CheckActivationCodeByMobile;

public record CheckActivationForUserCodeByMobileCommand(
    int CompanyId,
    string Mobile,
    string ActivationCode);