namespace Capitan360.Application.Features.Identities.Permissions.Dtos;

public class PermissionDto
{
    public Guid PermissionCode { get; set; }
    public string Name { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public bool Active { get; set; }
    public Guid ParentCode { get; set; }
    public string ParentName { get; set; } = default!;
    public string ParentDisplayName { get; set; } = default!;
}

//public class PermissionDto
//{
//    public string Name { get; set; } = default!;
//    public int Id { get; set; }
//    public string DisplayName { get; set; } = default!;
//    public string Parent { get; set; }
//    public string ParentDisplayName { get; set; }
//    public bool Active { get; set; }
//    public Guid PermissionCode { get; set; }
//    public Guid ParentCode { get; set; }
//}