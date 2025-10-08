namespace Capitan360.Application.Features.Identities.Old.Commands.RemoveUserPermission;

public record RemoveUserPermissionCommands
{
    public List<RemoveUserPermissionCommand> PermissionList { get; set; } = [];
}