namespace Capitan360.Application.Features.Companies.CompanyUris.Commands.Create;

public record CreateCompanyUriCommand(
    int CompanyId,
    string Uri,
    string Description,
    bool Active,
    bool Captain360Uri);