namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.Dtos;

public record CompanyInsuranceDto(
    int Id,
    string Code,
    string Name,
    decimal Tax,
    decimal Scale,
    string Description,
    bool Active,
    int CompanyTypeId,
    int CompanyId
);