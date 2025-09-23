namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateName;

public record UpdateCompanyPackageTypeNameCommand(
    string Name)
{
    public int Id { get; set; }
}