namespace Capitan360.Application.Features.UserPermission.Commands.AssignUserPermission;

public record AssignUserPermissionCommand
{
    public string UserId { get; set; }
    public int PermissionId { get; set; } = 0;
};