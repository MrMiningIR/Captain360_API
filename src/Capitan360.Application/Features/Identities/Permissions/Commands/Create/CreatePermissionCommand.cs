namespace Capitan360.Application.Features.Identities.Permissions.Commands.Create;

public record CreatePermissionCommand(
    Guid PermissionCode,
    string Name,
    string DisplayName,
    bool Active,
    Guid ParentCode,
    string ParentName,
    string ParentDisplayName);