namespace Capitan360.Application.Services.Identity.Commands.ChangeUserActivity;

public record ChangeUserActivityCommand
{
    public string UserId { get; set; } = default!;



}