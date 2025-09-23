namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Update;

public record UpdateCompanyInsuranceCommand(
    string Code,
    string Name,
    string CaptainCargoCode,
    decimal Tax,
    decimal Scale,
    string Description,
    bool Active
)
{
    public int Id { get; set; }
}