namespace Capitan360.Application.Services.UserPermission.Dtos;

public class UserPermissionDto
{
    public int Id { get; set; }
    public int PermissionId { get; set; }
    public string? PermissionName { get; set; }
    public string? DisplayPermissionName { get; set; }
    public Guid ParentCode { get; set; }

}