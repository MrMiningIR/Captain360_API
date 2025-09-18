namespace Capitan360.Application.Features.Companies.CompanyType.Commands.CreateCompanyType;

public record CreateCompanyTypeCommand(
    string TypeName,
    string DisplayName,
    string? Description);