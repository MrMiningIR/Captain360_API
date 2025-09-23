using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsuranceCharges.Commands.Update;

public record UpdateCompanyInsuranceChargeCommand(
    Rate Rate,
    decimal Value,
    decimal Settlement,
    bool IsPercent,
    bool Static,
    int CompanyInsuranceId
)
{
    public int Id { get; set; }
    public List<UpdateCompanyInsuranceChargePaymentCommand> SubOneChargePaymentCommand { get; set; } = [];
    public List<UpdateCompanyInsuranceChargePaymentContentTypeCommand> SubTwoChargePaymentContentTypeCommand { get; set; } = [];
}