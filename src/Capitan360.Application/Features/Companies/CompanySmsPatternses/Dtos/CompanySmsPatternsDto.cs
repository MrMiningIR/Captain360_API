namespace Capitan360.Application.Features.Companies.CompanySmsPatternses.Dtos;

public class CompanySmsPatternsDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public string SmsPanelUserName { get; set; } = default!;
    public string SmsPanelPassword { get; set; } = default!;
    public string SmsPanelNumber { get; set; } = default!;
    public string PatternSmsIssueSender { get; set; } = default!;
    public string PatternSmsIssueReceiver { get; set; } = default!;
    public string PatternSmsIssueCompany { get; set; } = default!;
    public string PatternSmsSendSenderPeakSender { get; set; } = default!;
    public string PatternSmsSendSenderPeakReceiver { get; set; } = default!;
    public string PatternSmsPackageInCompanySender { get; set; } = default!;
    public string PatternSmsPackageInCompanyReceiver { get; set; } = default!;
    public string PatternSmsManifestSender { get; set; } = default!;
    public string PatternSmsManifestReceiver { get; set; } = default!;
    public string PatternSmsReceivedInReceiverCompanySender { get; set; } = default!;
    public string PatternSmsReceivedInReceiverCompanyReceiver { get; set; } = default!;
    public string PatternSmsSendReceiverPeakSender { get; set; } = default!;
    public string PatternSmsSendReceiverPeakReceiver { get; set; } = default!;
    public string PatternSmsDeliverSender { get; set; } = default!;
    public string PatternSmsDeliverReceiver { get; set; } = default!;
    public string PatternSmsCancelSender { get; set; } = default!;
    public string PatternSmsCancelReceiver { get; set; } = default!;
    public string PatternSmsCancelByCustomerSender { get; set; } = default!;
    public string PatternSmsCancelByCustomerReceiver { get; set; } = default!;
    public string PatternSmsCancelByCustomerCompany { get; set; } = default!;
    public string PatternSmsSendManifestReceiverCompany { get; set; } = default!;
}