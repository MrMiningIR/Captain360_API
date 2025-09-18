namespace Capitan360.Application.Features.Companies.CompanyInsuranceCharges.Commands.Create;

public record CreateCompanyInsuranceChargeListCommand()
{
    public List<CreateCompanyInsuranceChargeCommand> CompanyInsuranceChargeList { get; set; } = [];
};