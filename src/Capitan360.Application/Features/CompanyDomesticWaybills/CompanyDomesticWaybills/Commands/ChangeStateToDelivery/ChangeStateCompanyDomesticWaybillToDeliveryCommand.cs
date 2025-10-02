namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToDelivery;

public record ChangeStateCompanyDomesticWaybillToDeliveryCommand(
    string CompanyReceiverDateFinancial,
    bool CompanyReceiverCashPayment,
    bool CompanyReceiverCashOnDelivery,
    bool CompanyReceiverBankPayment,
    int CompanyReceiverBankId,
    string CompanyReceiverBankPaymentNo,
    bool CompanyReceiverCreditPayment,
    string CompanyReceiverResponsibleCustomerId,
    string EntranceDeliveryPerson,
    string EntranceTransfereePersonName,
    string EntranceTransfereePersonNationalCode,
    string DescriptionReceiverCompany)
{
    public int Id { get; set; }
};






