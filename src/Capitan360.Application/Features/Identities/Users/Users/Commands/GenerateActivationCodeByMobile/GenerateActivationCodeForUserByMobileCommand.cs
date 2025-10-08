namespace Capitan360.Application.Features.Identities.Users.Users.Commands.GenerateActivationCodeByMobile;

public record GenerateActivationCodeForUserByMobileCommand(
    int CompanyId,
    int RoleId,
    string Mobile);
