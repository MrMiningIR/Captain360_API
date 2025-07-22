namespace Capitan360.Application.Services.UserPermission.Commands.AssignUserPermission;

public record AssignUserPermissionCommands
{
    public List<AssignUserPermissionCommand> PermissionList { get; set; } = [];
};