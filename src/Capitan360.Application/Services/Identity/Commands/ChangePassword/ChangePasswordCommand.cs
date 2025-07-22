namespace Capitan360.Application.Services.Identity.Commands.ChangePassword;

public record ChangePasswordCommand
{
    public string UserId { get; set; } = default!;
    public string NewPassword { get; set; } = default!;


}