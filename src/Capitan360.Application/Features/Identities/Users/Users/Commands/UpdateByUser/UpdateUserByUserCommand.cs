namespace Capitan360.Application.Features.Identities.Users.Users.Commands.UpdateByUser;

public record UpdateUserByUserCommand(
    string NameFamily,
    string MobileTelegram,
    string Tell,
    string Email)
{
    public string Id { get; set; }
};
