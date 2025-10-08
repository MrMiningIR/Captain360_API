namespace Capitan360.Application.Features.Identities.Users.Users.Commands.Register;

public record RegisterUserCommand(
    int CompanyId,
    int RoleId,
    string Mobile);
