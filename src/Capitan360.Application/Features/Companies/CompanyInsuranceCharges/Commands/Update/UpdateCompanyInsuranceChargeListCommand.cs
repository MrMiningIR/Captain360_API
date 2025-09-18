namespace Capitan360.Application.Features.Companies.CompanyInsuranceCharges.Commands.Update;

public record UpdateCompanyInsuranceChargeListCommand()
{
    public List<UpdateCompanyInsuranceChargeCommand> CompanyInsuranceChargeList { get; set; } = [];
};