using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.CreateCompanySmsPatterns;

public class CreateCompanySmsPatternsCommandValidator : AbstractValidator<CreateCompanySmsPatternsCommand>
{
    public CreateCompanySmsPatternsCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت الزامی است");

        RuleFor(x => x.PatternSmsIssueSender)
            .MaximumLength(500).WithMessage("الگوی SMS صدور فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsIssueSender != null);

        RuleFor(x => x.PatternSmsIssueReceiver)
            .MaximumLength(500).WithMessage("الگوی SMS صدور گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsIssueReceiver != null);

        RuleFor(x => x.PatternSmsIssueCompany)
            .MaximumLength(500).WithMessage("الگوی SMS صدور شرکت نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsIssueCompany != null);

        RuleFor(x => x.PatternSmsSendSenderPeakSender)
            .MaximumLength(500).WithMessage("الگوی SMS ارسال فرستنده در اوج فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsSendSenderPeakSender != null);

        RuleFor(x => x.PatternSmsSendSenderPeakReceiver)
            .MaximumLength(500).WithMessage("الگوی SMS ارسال فرستنده در اوج گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsSendSenderPeakReceiver != null);

        RuleFor(x => x.PatternSmsPackageInCompanySender)
            .MaximumLength(500).WithMessage("الگوی SMS بسته در شرکت فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsPackageInCompanySender != null);

        RuleFor(x => x.PatternSmsPackageInCompanyReceiver)
            .MaximumLength(500).WithMessage("الگوی SMS بسته در شرکت گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsPackageInCompanyReceiver != null);

        RuleFor(x => x.PatternSmsManifestSender)
            .MaximumLength(500).WithMessage("الگوی SMS مانیفست فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsManifestSender != null);

        RuleFor(x => x.PatternSmsManifestReceiver)
            .MaximumLength(500).WithMessage("الگوی SMS مانیفست گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsManifestReceiver != null);

        RuleFor(x => x.PatternSmsReceivedInReceiverCompanySender)
            .MaximumLength(500).WithMessage("الگوی SMS دریافت در شرکت گیرنده فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsReceivedInReceiverCompanySender != null);

        RuleFor(x => x.PatternSmsReceivedInReceiverCompanyReceiver)
            .MaximumLength(500).WithMessage("الگوی SMS دریافت در شرکت گیرنده گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsReceivedInReceiverCompanyReceiver != null);

        RuleFor(x => x.PatternSmsSendReceiverPeakSender)
            .MaximumLength(500).WithMessage("الگوی SMS ارسال گیرنده در اوج فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsSendReceiverPeakSender != null);

        RuleFor(x => x.PatternSmsSendReceiverPeakReceiver)
            .MaximumLength(500).WithMessage("الگوی SMS ارسال گیرنده در اوج گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsSendReceiverPeakReceiver != null);

        RuleFor(x => x.PatternSmsDeliverSender)
            .MaximumLength(500).WithMessage("الگوی SMS تحویل فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsDeliverSender != null);

        RuleFor(x => x.PatternSmsDeliverReceiver)
            .MaximumLength(500).WithMessage("الگوی SMS تحویل گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsDeliverReceiver != null);

        RuleFor(x => x.PatternSmsCancelSender)
            .MaximumLength(500).WithMessage("الگوی SMS لغو فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsCancelSender != null);

        RuleFor(x => x.PatternSmsCancelReceiver)
            .MaximumLength(500).WithMessage("الگوی SMS لغو گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsCancelReceiver != null);

        RuleFor(x => x.PatternSmsCancelByCustomerSender)
            .MaximumLength(500).WithMessage("الگوی SMS لغو توسط مشتری فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsCancelByCustomerSender != null);

        RuleFor(x => x.PatternSmsCancelByCustomerReceiver)
            .MaximumLength(500).WithMessage("الگوی SMS لغو توسط مشتری گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsCancelByCustomerReceiver != null);

        RuleFor(x => x.PatternSmsCancelByCustomerCompany)
            .MaximumLength(500).WithMessage("الگوی SMS لغو توسط مشتری شرکت نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsCancelByCustomerCompany != null);

        RuleFor(x => x.PatternSmsSendManifestReceiverCompany)
            .MaximumLength(500).WithMessage("الگوی SMS ارسال مانیفست شرکت گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد")
            .When(x => x.PatternSmsSendManifestReceiverCompany != null);
    }
}