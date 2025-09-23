namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.Update;

public record UpdateCompanyPackageTypeCommand(
    string Name,
    bool Active,
    string Description
    )
{
    public int Id { get; set; }
}