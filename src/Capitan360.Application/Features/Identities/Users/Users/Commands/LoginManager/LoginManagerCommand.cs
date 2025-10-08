namespace Capitan360.Application.Features.Identities.Users.Users.Commands.LoginManager;

public record LoginManagerCommand(
    int CompanyId,
    string Mobile,
    string Password);
