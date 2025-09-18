namespace Capitan360.Application.Features.UserPermission.Commands.UpDeInlUserPermissionById;

public record UpDeInlUserPermissionByIdsCommand
{
    public List<string> PermissionIds { get; set; } = [];
    public string? UserId { get; set; }
}