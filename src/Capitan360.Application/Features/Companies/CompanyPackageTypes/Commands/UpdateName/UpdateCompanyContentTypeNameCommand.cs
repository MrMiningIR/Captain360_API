namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateName;

public record UpdateCompanyContentTypeNameCommand(
    string Name)
{
    public int Id { get; set; }
}
