namespace Capitan360.Domain.Dtos;

public record PermissionInfo
{
    public int PermissionId { get; set; }
    public string PermissionName { get; set; }
    public string UserId { get; set; }
}

