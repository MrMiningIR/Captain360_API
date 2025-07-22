namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.UpdateCompanyInsurance;

public record UpdateCompanyInsuranceCommand(
    string Code,
    string Name,
    decimal Tax,
    decimal Scale,
    string Description,
    bool Active,
    int CompanyTypeId,
    int CompanyId
)
{
    public int Id { get; set; }
}