namespace Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateCompanyPackageType;

public record UpdateCompanyPackageTypeCommand(
    string CompanyPackageTypeName,
    bool CompanyPackageTypeActive,
    string? CompanyPackageTypeDescription
    )
{
    public int Id { get; set; }
}