namespace Capitan360.Application.Services.UserPermission.Commands.RemoveUserPermission;

public record RemoveUserPermissionCommand
{
    public string UserId { get; set; }
    public int PermissionId { get; set; } = 0;
}