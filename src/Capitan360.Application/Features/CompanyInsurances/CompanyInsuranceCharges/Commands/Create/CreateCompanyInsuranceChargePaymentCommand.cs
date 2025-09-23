using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsuranceCharges.Commands.Create;

public record CreateCompanyInsuranceChargePaymentCommand(
    Rate Rate,
    int CompanyInsuranceChargeId,
    decimal Diff,
    bool IsPercent);