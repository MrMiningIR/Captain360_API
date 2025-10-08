namespace Capitan360.Application.Features.Identities.Users.Users.Commands.GenerateRecoveryPasswordCode;

public record GenerateRecoveryPasswordCodeForUserCommand(
    string Id,
    string Mobile);
