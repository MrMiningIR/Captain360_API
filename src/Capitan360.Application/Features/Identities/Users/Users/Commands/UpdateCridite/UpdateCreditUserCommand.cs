namespace Capitan360.Application.Features.Identities.Users.Users.Commands.UpdateCridite;

public record UpdateCreditUserCommand(
    long Credit,
    bool HasCredit)
{
    public string Id { get; set; } = default!;
};
