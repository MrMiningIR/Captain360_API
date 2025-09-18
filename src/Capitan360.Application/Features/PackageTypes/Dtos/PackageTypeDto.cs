namespace Capitan360.Application.Features.PackageTypes.Dtos;

public class PackageTypeDto
{
    public int Id { get; set; }
    public int CompanyTypeId { get; set; }
    public string? CompanyTypeName { get; set; }
    public string Name { get; set; } = default!;
    public bool Active { get; set; }
    public string Description { get; set; } = default!;
    public int Order { get; set; }
}