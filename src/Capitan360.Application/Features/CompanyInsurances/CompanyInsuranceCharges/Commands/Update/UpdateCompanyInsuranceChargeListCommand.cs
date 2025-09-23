namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsuranceCharges.Commands.Update;

public record UpdateCompanyInsuranceChargeListCommand()
{
    public List<UpdateCompanyInsuranceChargeCommand> CompanyInsuranceChargeList { get; set; } = [];
};