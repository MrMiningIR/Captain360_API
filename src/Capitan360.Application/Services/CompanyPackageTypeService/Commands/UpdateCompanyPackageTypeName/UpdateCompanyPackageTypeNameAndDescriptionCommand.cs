namespace Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateCompanyPackageTypeName;

public record UpdateCompanyPackageTypeNameAndDescriptionCommand
{
    public int Id { get; set; }
    public int OriginalPackageId { get; set; }
    public string PackageTypeName { get; set; } = default!;
    public string? PackageTypeDescription { get; set; }

}