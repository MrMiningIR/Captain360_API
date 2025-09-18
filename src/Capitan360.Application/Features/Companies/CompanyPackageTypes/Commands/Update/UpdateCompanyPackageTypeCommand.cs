namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.Update;

public record UpdateCompanyPackageTypeCommand(
    string CompanyPackageTypeName,
    bool CompanyPackageTypeActive,
    string? CompanyPackageTypeDescription
    )
{
    public int Id { get; set; }
}