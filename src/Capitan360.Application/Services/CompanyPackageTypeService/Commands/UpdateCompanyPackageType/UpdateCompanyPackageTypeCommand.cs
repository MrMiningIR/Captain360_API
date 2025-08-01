namespace Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateCompanyPackageType;

public record UpdateCompanyPackageTypeCommand
{
    public int Id { get; set; }
    public int PackageTypeId { get; set; }
    public int CompanyId { get; set; }
    public string CompanyPackageTypeName { get; set; } = default!;
    public bool? Active { get; set; }
}