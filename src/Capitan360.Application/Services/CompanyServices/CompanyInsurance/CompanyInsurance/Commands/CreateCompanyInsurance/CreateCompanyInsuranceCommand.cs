namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.CreateCompanyInsurance;

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