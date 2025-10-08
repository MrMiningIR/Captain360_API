using Capitan360.Domain.Entities.Identities;

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