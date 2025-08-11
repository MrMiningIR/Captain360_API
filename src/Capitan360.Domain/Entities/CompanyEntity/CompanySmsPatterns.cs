using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.CompanyEntity
{
    public class CompanySmsPatterns : Entity
    {
        public string? PatternSmsIssueSender { get; set; }

        public string? PatternSmsIssueReceiver { get; set; }

        public string? PatternSmsIssueCompany { get; set; }

        public string? PatternSmsSendSenderPeakSender { get; set; }


        public string? PatternSmsSendSenderPeakReceiver { get; set; }


        public string? PatternSmsPackageInCompanySender { get; set; }


        public string? PatternSmsPackageInCompanyReceiver { get; set; }


        public string? PatternSmsManifestSender { get; set; }


        public string? PatternSmsManifestReceiver { get; set; }


        public string? PatternSmsReceivedInReceiverCompanySender { get; set; }


        public string? PatternSmsReceivedInReceiverCompanyReceiver { get; set; }


        public string? PatternSmsSendReceiverPeakSender { get; set; }


        public string? PatternSmsSendReceiverPeakReceiver { get; set; }


        public string? PatternSmsDeliverSender { get; set; }


        public string? PatternSmsDeliverReceiver { get; set; }


        public string? PatternSmsCancelSender { get; set; }


        public string? PatternSmsCancelReceiver { get; set; }


        public string? PatternSmsCancelByCustomerSender { get; set; }


        public string? PatternSmsCancelByCustomerReceiver { get; set; }


        public string? PatternSmsCancelByCustomerCompany { get; set; }


        public string? PatternSmsSendManifestReceiverCompany { get; set; }


        // Navigation Properties
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
