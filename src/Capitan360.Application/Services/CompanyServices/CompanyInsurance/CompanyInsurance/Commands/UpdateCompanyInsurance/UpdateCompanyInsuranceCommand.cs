namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.UpdateCompanyInsurance;

public record UpdateCompanyInsuranceCommand(
    string Code,
    string Name,
    string? CaptainCargoCode,
    decimal Tax,
    decimal Scale,
    string? Description,
    bool Active
)
{
    public int Id { get; set; }
}