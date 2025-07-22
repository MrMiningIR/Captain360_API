namespace Capitan360.Application.Services.UserPermission.Commands.RemoveUserPermission;

public record RemoveUserPermissionCommands
{
    public List<RemoveUserPermissionCommand> PermissionList { get; set; } = [];
}