namespace Capitan360.Application.Features.Identities.Users.Users.Commands.UpdatePermissionVersion;

public record UpdatePermissionVersionCommand(
    string PermissionVersion)
{
    public string Id { get; set; } = default!;
};
