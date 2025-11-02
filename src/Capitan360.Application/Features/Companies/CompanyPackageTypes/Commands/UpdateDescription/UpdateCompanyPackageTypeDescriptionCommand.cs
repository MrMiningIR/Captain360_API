namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateDescription;

public record UpdateCompanyPackageTypeDescriptionCommand(string CompanyPackageTypeDescription)
{
    public int Id { get; set; }



}