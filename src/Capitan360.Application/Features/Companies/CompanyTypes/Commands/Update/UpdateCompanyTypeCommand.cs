namespace Capitan360.Application.Features.Companies.CompanyType.Commands.UpdateCompanyType;

public record UpdateCompanyTypeCommand(
    string TypeName,
    string DisplayName,
    string? Description)
{
    public int Id { get; set; }
}