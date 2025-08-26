namespace Capitan360.Application.Services.CompanyPackageTypeService.Dtos;

public class CompanyPackageTypeDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public int PackageTypeId { get; set; }
    public string? PackageTypeName { get; set; } 
    public string CompanyPackageTypeName { get; set; } = default!;
    public bool CompanyPackageTypeActive { get; set; }
    public int CompanyPackageTypeOrder { get; set; }
    public string? CompanyPackageTypeDescription { get; set; }
}