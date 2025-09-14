using Capitan360.Domain.Enums;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.UpdateCompanyInsuranceCharge;

public record UpdateCompanyInsuranceChargePaymentCommand(
   
    Rate Rate,
    int CompanyInsuranceChargeId,
    decimal Diff,
    bool IsPercent)
{
    public int Id { get; set; }
};