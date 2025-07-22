using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.UpdateCompanyInsuranceCharge;

public record UpdateCompanyInsuranceChargePaymentContentTypeCommand(
    int Id,
    Rate Rate,
    int CompanyInsuranceChargeId,
    int ContentId,
    decimal RateSettlement,
    bool IsPercentRateSettlement,
    decimal RateDiff,
    bool IsPercentDiff
)
{
    public int Id { get; set; }
};