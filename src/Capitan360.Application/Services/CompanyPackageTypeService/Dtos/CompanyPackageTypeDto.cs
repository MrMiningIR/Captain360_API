namespace Capitan360.Application.Services.CompanyPackageTypeService.Dtos;

public class CompanyPackageTypeDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int PackageTypeId { get; set; }
    public string CompanyPackageTypeName { get; set; } = default!;
    public string NewCompanyPackageTypeName { get; set; } = default!;
    public string CompanyPackageTypeDescription { get; set; } = default!;
    public bool Active { get; set; }
    public int OrderCompanyPackageType { get; set; }
}