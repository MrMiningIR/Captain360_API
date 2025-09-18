namespace Capitan360.Application.Features.Companies.CompanyUri.Commands.CreateCompanyUri;

public record CreateCompanyUriCommand(
    int CompanyId,
    string Uri,
    string? Description,
    bool Active,
    bool Captain360Uri);