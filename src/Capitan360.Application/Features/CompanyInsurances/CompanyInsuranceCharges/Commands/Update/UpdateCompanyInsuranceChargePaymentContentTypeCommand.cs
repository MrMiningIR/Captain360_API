using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsuranceCharges.Commands.Update;

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