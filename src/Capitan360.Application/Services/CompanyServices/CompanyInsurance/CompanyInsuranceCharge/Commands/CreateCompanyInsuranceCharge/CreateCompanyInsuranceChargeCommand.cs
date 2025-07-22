using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.CreateCompanyInsuranceCharge;


public record CreateCompanyInsuranceChargeCommand(
    Rate Rate,
    decimal Value,
    decimal Settlement,
    bool IsPercent,
    bool Static,
    int CompanyInsuranceId
)
{
    public List<CreateCompanyInsuranceChargePaymentCommand> SubOneChargePaymentCommand { get; set; } = [];
    public List<CreateCompanyInsuranceChargePaymentContentTypeCommand> SubTwoChargePaymentContentTypeCommand { get; set; } = [];
}