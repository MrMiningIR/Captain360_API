namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToDeliveryFromDesktop;

public record ChangeStateToDeliveryFromDesktopCommand(
    long No,
    string CompanyReceiverCaptain360Code,
    string CompanyReceiverDateFinancial,
    bool CompanyReceiverCashPayment,
    bool CompanyReceiverCashOnDelivery,
    bool CompanyReceiverBankPayment,
    string CompanyReceiverBankCode,
    string CompanyReceiverBankName,
    string CompanyReceiverBankPaymentNo,
    bool CompanyReceiverCreditPayment,
    string CompanyReceiverResponsibleCustomerId,
    string EntranceDeliveryPerson,
    string EntranceTransfereePersonName,
    string EntranceTransfereePersonNationalCode,
    string DescriptionReceiverCompany,
    string DateDelivery,
    string TimeDelivery);
