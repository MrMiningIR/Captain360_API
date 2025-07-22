namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.CreateCompanyInsurance;

public record CreateCompanyInsuranceCommand(
    string Code,
    string Name,
    decimal? Tax,
    decimal? Scale,
    string Description,
    bool Active,
    int CompanyTypeId,
    int CompanyId
);