namespace Capitan360.Application.Features.Companies.CompanyTypes.Commands.Update;

public record UpdateCompanyTypeCommand(
    string TypeName,
    string DisplayName,
    string Description)
{
    public int Id { get; set; }
}