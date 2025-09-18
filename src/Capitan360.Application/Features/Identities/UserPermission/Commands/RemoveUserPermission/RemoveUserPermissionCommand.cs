namespace Capitan360.Application.Features.UserPermission.Commands.RemoveUserPermission;

public record RemoveUserPermissionCommand
{
    public string UserId { get; set; }
    public int PermissionId { get; set; } = 0;
}