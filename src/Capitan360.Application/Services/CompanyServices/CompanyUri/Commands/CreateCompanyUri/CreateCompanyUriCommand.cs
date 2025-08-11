namespace Capitan360.Application.Services.CompanyServices.CompanyUri.Commands.CreateCompanyUri;

public record CreateCompanyUriCommand(
    int CompanyId,
    string Uri,
    string? Description,
    bool Active);