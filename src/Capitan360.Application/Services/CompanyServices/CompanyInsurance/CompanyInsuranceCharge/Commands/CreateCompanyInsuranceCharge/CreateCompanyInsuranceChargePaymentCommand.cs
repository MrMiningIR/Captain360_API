using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.CreateCompanyInsuranceCharge;

public record CreateCompanyInsuranceChargePaymentCommand(
    Rate Rate,
    int CompanyInsuranceChargeId,
    decimal Diff,
    bool IsPercent);