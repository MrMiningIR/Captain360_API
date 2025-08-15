namespace Capitan360.Application.Services.PackageTypeService.Dtos;

public class PackageTypeDto
{
    public int Id { get; set; }
    public int CompanyTypeId { get; set; }
    public string CompanyTypeName { get; set; } = default!;
    public string PackageTypeName { get; set; } = default!;
    public bool PackageTypeActive { get; set; }
    public string PackageTypeDescription { get; set; } = default!;
    public int PackageTypeOrder { get; set; }
}