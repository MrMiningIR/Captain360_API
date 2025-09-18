namespace Capitan360.Application.Features.UserPermission.Commands.RemoveUserPermission;

public record RemoveUserPermissionCommands
{
    public List<RemoveUserPermissionCommand> PermissionList { get; set; } = [];
}