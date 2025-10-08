namespace Capitan360.Application.Features.Identities.Permissions.Commands.Update;

public record UpdatePermissionCommand(
    Guid PermissionCode,
    string Name,
    string DisplayName,
    bool Active,
    Guid ParentCode,
    string ParentName,
    string ParentDisplayName)
{
    public int Id { get; set; }
};
