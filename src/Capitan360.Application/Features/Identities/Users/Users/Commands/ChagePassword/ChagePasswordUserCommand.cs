namespace Capitan360.Application.Features.Identities.Users.Users.Commands.ChagePassword;

public record ChagePasswordUserCommand(
    string CurrentPassword,
    string NewPassword,
    string ConfirmNewPassword)
{
    public string Id { get; set; }
};
