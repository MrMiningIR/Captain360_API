namespace Capitan360.Application.Features.Identities.Users.Users.Commands.CheckRecoveryPasswordCode;

public record CheckRecoveryPasswordCodeForUserCommand(
    string Id,
    string RecoveryPasswordCode);
