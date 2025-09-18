namespace Capitan360.Application.Features.UserPermission.Commands.AssignUserPermission;

public record AssignUserPermissionCommands
{
    public List<AssignUserPermissionCommand> PermissionList { get; set; } = [];
};