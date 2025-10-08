namespace Capitan360.Application.Features.Identities.Users.Users.Commands.LoginUser;

public record LoginUserCommand(
    int CompanyId,
    string Mobile,
    string Password);