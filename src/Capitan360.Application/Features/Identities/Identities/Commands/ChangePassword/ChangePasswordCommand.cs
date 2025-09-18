namespace Capitan360.Application.Features.Identities.Identities.Commands.ChangePassword;

public record ChangePasswordCommand
{
    public string UserId { get; set; } = default!;
    public string NewPassword { get; set; } = default!;


}