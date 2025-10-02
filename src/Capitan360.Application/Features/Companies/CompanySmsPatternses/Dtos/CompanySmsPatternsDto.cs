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
    public bool ActivePatternSmsIssueSender { get; set; }
    public string PatternSmsIssueReceiver { get; set; } = default!;
    public bool ActivePatternSmsIssueReceiver { get; set; }
    public string PatternSmsIssueCompany { get; set; } = default!;
    public bool ActivePatternSmsIssueCompany { get; set; }
    public string PatternSmsSendSenderPeakSender { get; set; } = default!;
    public bool ActivePatternSmsSendSenderPeakSender { get; set; }
    public string PatternSmsSendSenderPeakReceiver { get; set; } = default!;
    public bool ActivePatternSmsSendSenderPeakReceiver { get; set; }
    public string PatternSmsPackageInCompanySender { get; set; } = default!;
    public bool ActivePatternSmsPackageInCompanySender { get; set; }
    public string PatternSmsPackageInCompanyReceiver { get; set; } = default!;
    public bool ActivePatternSmsPackageInCompanyReceiver { get; set; }
    public string PatternSmsManifestSender { get; set; } = default!;
    public bool ActivePatternSmsManifestSender { get; set; }
    public string PatternSmsManifestReceiver { get; set; } = default!;
    public bool ActivePatternSmsManifestReceiver { get; set; }
    public string PatternSmsReceivedInReceiverCompanySender { get; set; } = default!;
    public bool ActivePatternSmsReceivedInReceiverCompanySender { get; set; }
    public string PatternSmsReceivedInReceiverCompanyReceiver { get; set; } = default!;
    public bool ActivePatternSmsReceivedInReceiverCompanyReceiver { get; set; }
    public string PatternSmsSendReceiverPeakSender { get; set; } = default!;
    public bool ActivePatternSmsSendReceiverPeakSender { get; set; }
    public string PatternSmsSendReceiverPeakReceiver { get; set; } = default!;
    public bool ActivePatternSmsSendReceiverPeakReceiver { get; set; }
    public string PatternSmsDeliverSender { get; set; } = default!;
    public bool ActivePatternSmsDeliverSender { get; set; }
    public string PatternSmsDeliverReceiver { get; set; } = default!;
    public bool ActivePatternSmsDeliverReceiver { get; set; }
    public string PatternSmsCancelSender { get; set; } = default!;
    public bool ActivePatternSmsCancelSender { get; set; }
    public string PatternSmsCancelReceiver { get; set; } = default!;
    public bool ActivePatternSmsCancelReceiver { get; set; }
    public string PatternSmsCancelByCustomerSender { get; set; } = default!;
    public bool ActivePatternSmsCancelByCustomerSender { get; set; }
    public string PatternSmsCancelByCustomerReceiver { get; set; } = default!;
    public bool ActivePatternSmsCancelByCustomerReceiver { get; set; }
    public string PatternSmsCancelByCustomerCompany { get; set; } = default!;
    public bool ActivePatternSmsCancelByCustomerCompany { get; set; }
    public string PatternSmsSendManifestReceiverCompany { get; set; } = default!;
    public bool ActivePatternSmsSendManifestReceiverCompany { get; set; }
}