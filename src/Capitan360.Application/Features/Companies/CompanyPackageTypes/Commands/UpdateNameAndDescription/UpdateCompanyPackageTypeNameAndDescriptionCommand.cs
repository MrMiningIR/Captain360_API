namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateNameAndDescription;

public record UpdateCompanyPackageTypeNameAndDescriptionCommand(string CompanyPackageTypeName, string? CompanyPackageTypeDescription)
{
    public int Id { get; set; }



}