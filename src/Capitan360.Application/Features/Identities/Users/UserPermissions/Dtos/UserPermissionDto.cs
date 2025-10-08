namespace Capitan360.Application.Features.Identities.Users.UserPermissions.Dtos;

public class UserPermissionDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public string? UserNameFamily { get; set; }
    public int PermissionId { get; set; }
    public string? PermissionDisplayName { get; set; } = default!;
}