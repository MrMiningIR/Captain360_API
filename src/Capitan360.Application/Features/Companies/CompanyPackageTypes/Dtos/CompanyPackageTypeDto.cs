namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Dtos;

public class CompanyPackageTypeDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public int PackageTypeId { get; set; }
    public string? PackageTypeName { get; set; }
    public string Name { get; set; } = default!;
    public bool Active { get; set; }
    public int Order { get; set; }
    public string Description { get; set; } = default!;
}