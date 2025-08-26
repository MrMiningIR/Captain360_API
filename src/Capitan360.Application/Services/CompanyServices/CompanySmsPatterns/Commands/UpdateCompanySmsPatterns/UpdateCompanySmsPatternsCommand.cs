namespace Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.UpdateCompanySmsPatterns;

public record UpdateCompanySmsPatternsCommand(
    string? SmsPanelUserName,
    string? SmsPanelPassword,
    string? SmsPanelNumber,
    string? PatternSmsIssueSender,
    string? PatternSmsIssueReceiver,
    string? PatternSmsIssueCompany,
    string? PatternSmsSendSenderPeakSender,
    string? PatternSmsSendSenderPeakReceiver,
    string? PatternSmsPackageInCompanySender,
    string? PatternSmsPackageInCompanyReceiver,
    string? PatternSmsManifestSender,
    string? PatternSmsManifestReceiver,
    string? PatternSmsReceivedInReceiverCompanySender,
    string? PatternSmsReceivedInReceiverCompanyReceiver,
    string? PatternSmsSendReceiverPeakSender,
    string? PatternSmsSendReceiverPeakReceiver,
    string? PatternSmsDeliverSender,
    string? PatternSmsDeliverReceiver,
    string? PatternSmsCancelSender,
    string? PatternSmsCancelReceiver,
    string? PatternSmsCancelByCustomerSender,
    string? PatternSmsCancelByCustomerReceiver,
    string? PatternSmsCancelByCustomerCompany,
    string? PatternSmsSendManifestReceiverCompany)
{
    public int Id { get; set; }
}