namespace Capitan360.Application.Features.Companies.CompanyInsurances.Commands.Create;

public record CreateCompanyInsuranceCommand(
    int CompanyId,
    string Code,
    string Name,
    string? CaptainCargoCode,
    decimal Tax,
    decimal Scale,
    string? Description,
    bool Active
);