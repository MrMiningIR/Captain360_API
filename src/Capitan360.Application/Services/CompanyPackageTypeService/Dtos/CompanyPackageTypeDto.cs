namespace Capitan360.Application.Services.CompanyPackageTypeService.Dtos;

public class CompanyPackageTypeDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int PackageTypeId { get; set; }
    public string PackageTypeName { get; set; } = default!;
    public string NewCompanyPackageTypeName { get; set; } = default!;
    public string CompanyPackageTypeDescription { get; set; } = default!;
    public bool Active { get; set; }
    public int OrderPackageType { get; set; }
}