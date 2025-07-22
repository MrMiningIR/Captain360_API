namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.CreateCompanyInsuranceCharge;

public record CreateCompanyInsuranceChargeListCommand()
{
    public List<CreateCompanyInsuranceChargeCommand> CompanyInsuranceChargeList { get; set; } = [];
};