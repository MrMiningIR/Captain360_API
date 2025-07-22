namespace Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateCompanyPackageType;

public record UpdateCompanyPackageTypeNameCommand
{
    public int Id { get; set; }
    public int OriginalPackageId { get; set; }
    public string PackageTypeName { get; set; } = default!;

}