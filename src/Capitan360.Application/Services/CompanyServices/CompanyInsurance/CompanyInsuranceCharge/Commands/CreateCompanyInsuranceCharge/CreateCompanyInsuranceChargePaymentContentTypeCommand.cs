using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.CreateCompanyInsuranceCharge;

public record CreateCompanyInsuranceChargePaymentContentTypeCommand(
    Rate Rate,
    int CompanyInsuranceChargeId,
    int ContentId,
    decimal RateSettlement,
    bool IsPercentRateSettlement,
    decimal RateDiff,
    bool IsPercentDiff
);