namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.UpdateCompanyInsuranceCharge;

public record UpdateCompanyInsuranceChargeListCommand()
{
    public List<UpdateCompanyInsuranceChargeCommand> CompanyInsuranceChargeList { get; set; } = [];
};