using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsuranceCharges.Commands.Update;

public record UpdateCompanyInsuranceChargePaymentCommand(

    Rate Rate,
    int CompanyInsuranceChargeId,
    decimal Diff,
    bool IsPercent)
{
    public int Id { get; set; }
};