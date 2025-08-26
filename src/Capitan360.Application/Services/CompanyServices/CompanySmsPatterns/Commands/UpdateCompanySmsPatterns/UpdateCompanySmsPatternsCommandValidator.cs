using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.UpdateCompanySmsPatterns;

public class UpdateCompanySmsPatternsCommandValidator : AbstractValidator<UpdateCompanySmsPatternsCommand>
{
    public UpdateCompanySmsPatternsCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه الگوهای SMS باید مشخص باشد");

        RuleFor(x => x.SmsPanelUserName)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.SmsPanelUserName))
            .WithMessage("نام کاربری پنل SMS نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.SmsPanelPassword)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.SmsPanelPassword))
            .WithMessage("رمز عبور پنل SMS نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.SmsPanelNumber)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.SmsPanelNumber))
            .WithMessage("شماره پنل SMS نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.PatternSmsIssueSender)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsIssueSender))
            .WithMessage("الگوی SMS صدور فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsIssueReceiver)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsIssueReceiver))
            .WithMessage("الگوی SMS صدور گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsIssueCompany)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsIssueCompany))
            .WithMessage("الگوی SMS صدور شرکت نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsSendSenderPeakSender)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsSendSenderPeakSender))
            .WithMessage("الگوی SMS ارسال پیک شرکت فرستنده برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsSendSenderPeakReceiver)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsSendSenderPeakReceiver))
            .WithMessage("الگوی SMS ارسال پیک شرکت فرستنده برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsPackageInCompanySender)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsPackageInCompanySender))
            .WithMessage("الگوی SMS رسیدن بسته به شرکت فرستنده برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsPackageInCompanyReceiver)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsPackageInCompanyReceiver))
            .WithMessage("الگوی SMS رسیدن بسته به شرکت فرستنده برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsManifestSender)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsManifestSender))
            .WithMessage("الگوی SMS مانیفست برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsManifestReceiver)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsManifestReceiver))
            .WithMessage("الگوی SMS مانیفست برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsReceivedInReceiverCompanySender)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsReceivedInReceiverCompanySender))
            .WithMessage("الگوی SMS دریافت در شرکت گیرنده برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsReceivedInReceiverCompanyReceiver)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsReceivedInReceiverCompanyReceiver))
            .WithMessage("الگوی SMS دریافت در شرکت گیرنده برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsSendReceiverPeakSender)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsSendReceiverPeakSender))
            .WithMessage("الگوی SMS ارسال پیک شرکت گیرنده برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsSendReceiverPeakReceiver)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsSendReceiverPeakReceiver))
            .WithMessage("الگوی SMS ارسال پیک شرکت گیرنده برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsDeliverSender)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsDeliverSender))
            .WithMessage("الگوی SMS تحویل برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsDeliverReceiver)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsDeliverReceiver))
            .WithMessage("الگوی SMS تحویل برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsCancelSender)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsCancelSender))
            .WithMessage("الگوی SMS لغو برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsCancelReceiver)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsCancelReceiver))
            .WithMessage("الگوی SMS لغو برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsCancelByCustomerSender)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsCancelByCustomerSender))
            .WithMessage("الگوی SMS لغو توسط مشتری برای شرکت فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsCancelByCustomerReceiver)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsCancelByCustomerReceiver))
            .WithMessage("الگوی SMS لغو توسط مشتری برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsCancelByCustomerCompany)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsCancelByCustomerCompany))
            .WithMessage("الگوی SMS لغو توسط مشتری برای شرکت نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsSendManifestReceiverCompany)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PatternSmsSendManifestReceiverCompany))
            .WithMessage("الگوی SMS ارسال مانیفست برای شرکت گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}