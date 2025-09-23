namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsuranceCharges.Commands.Create;

public record CreateCompanyInsuranceChargeListCommand()
{
    public List<CreateCompanyInsuranceChargeCommand> CompanyInsuranceChargeList { get; set; } = [];
};