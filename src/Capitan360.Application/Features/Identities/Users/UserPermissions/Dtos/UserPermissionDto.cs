namespace Capitan360.Application.Features.Identities.Users.UserPermissions.Dtos;

public class UserPermissionDto
{
    public int Id { get; set; }
    public int PermissionId { get; set; }
    public string? PermissionName { get; set; }
    public Guid ParentCode { get; set; }
    public string? DisplayPermissionName { get; set; } = default!;
}