using Capitan360.Domain.Enums;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.CreateCompanyInsuranceCharge;

public record CreateCompanyInsuranceChargePaymentCommand(
    Rate Rate,
    int CompanyInsuranceChargeId,
    decimal Diff,
    bool IsPercent);