using Capitan360.Application.Services.Dtos;

namespace Capitan360.Application.Services.Identity.Dtos;

public class UserDto
{
    public string Id { get; set; }
    public string? FullName { get; set; }
    public DateTime LastAccess { get; set; }
    public bool Active { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? CompanyName { get; set; }
    public int? MoadianFactorType { get; set; }
    public int? CompanyId { get; set; }
    public int UserKind { get; set; }
    public int CompanyTypeId { get; set; }
    public ICollection<RoleDto>? Roles { get; set; } = [];
}