namespace Capitan360.Application.Features.Companies.CompanyTypes.Commands.Create;

public record CreateCompanyTypeCommand(
    string TypeName,
    string DisplayName,
    string Description);