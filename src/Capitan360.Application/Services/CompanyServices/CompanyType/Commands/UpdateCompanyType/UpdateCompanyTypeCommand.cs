namespace Capitan360.Application.Services.CompanyServices.CompanyType.Commands.UpdateCompanyType;

public record UpdateCompanyTypeCommand(
    int Id,
    string? TypeName,
    string? DisplayName,
    string? Description);