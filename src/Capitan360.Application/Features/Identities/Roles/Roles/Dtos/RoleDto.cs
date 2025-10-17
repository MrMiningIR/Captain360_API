namespace Capitan360.Application.Features.Identities.Roles.Roles.Dtos;

public class RoleDto
{
    public string Id { get; set; }
    public string Name { get; set; } = default!;
    public string PersianName { get; set; } = default!;
    public bool Visible { get; set; }
}
