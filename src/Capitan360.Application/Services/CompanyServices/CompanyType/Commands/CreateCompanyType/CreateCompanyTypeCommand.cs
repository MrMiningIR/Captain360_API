namespace Capitan360.Application.Services.CompanyServices.CompanyType.Commands.CreateCompanyType;

public record CreateCompanyTypeCommand(
    string TypeName,
    string DisplayName,
    string? Description);