using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsuranceCharges.Commands.Create;

public record CreateCompanyInsuranceChargePaymentContentTypeCommand(
    Rate Rate,
    int CompanyInsuranceChargeId,
    int ContentId,
    decimal RateSettlement,
    bool IsPercentRateSettlement,
    decimal RateDiff,
    bool IsPercentDiff
);