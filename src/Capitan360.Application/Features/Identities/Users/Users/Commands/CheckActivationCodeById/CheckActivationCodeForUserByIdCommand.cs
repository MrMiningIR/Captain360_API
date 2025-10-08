namespace Capitan360.Application.Features.Identities.Users.Users.Commands.CheckActivationCodeById;

public record CheckActivationCodeForUserByIdCommand(
    string Id,
    string ActivationCode);
