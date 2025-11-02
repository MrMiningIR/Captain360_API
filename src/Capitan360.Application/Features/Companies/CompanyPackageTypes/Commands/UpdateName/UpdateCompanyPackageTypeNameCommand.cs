namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateName;

public record UpdateCompanyPackageTypeNameCommand(
    string Name)
{
    public int Id { get; set; } = 0;
}
