using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanySmsPatternses.Commands.Create;

public class CreateCompanySmsPatternsCommandValidator : AbstractValidator<CreateCompanySmsPatternsCommand>
{
    public CreateCompanySmsPatternsCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت الزامی است");

        RuleFor(x => x.SmsPanelUserName)
            .NotEmpty().WithMessage("نام کاربری پنل SMS الزامی است.")
            .MaximumLength(100).WithMessage("نام کاربری پنل SMS نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.SmsPanelPassword)
            .NotEmpty().WithMessage("رمز عبور پنل SMS الزامی است.")
            .MaximumLength(100).WithMessage("رمز عبور پنل SMS نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.SmsPanelNumber)
            .NotEmpty().WithMessage("شماره پنل SMS الزامی است.")
            .MaximumLength(100).WithMessage("شماره پنل SMS نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.PatternSmsIssueSender)
            .NotEmpty().WithMessage("الگوی SMS صدور برای فرستنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS صدور برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsIssueReceiver)
            .NotEmpty().WithMessage("الگوی SMS صدور برای گیرنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS صدور برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsIssueCompany)
            .NotEmpty().WithMessage("الگوی SMS صدور برای شرکت الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS صدور برای شرکت نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsSendSenderPeakSender)
            .NotEmpty().WithMessage("الگوی SMS ارسال پیک شرکت فرستنده برای فرستنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS ارسال پیک شرکت فرستنده برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsSendSenderPeakReceiver)
            .NotEmpty().WithMessage("الگوی SMS ارسال پیک شرکت فرستنده برای گیرنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS ارسال پیک شرکت فرستنده برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsPackageInCompanySender)
            .NotEmpty().WithMessage("الگوی SMS رسیدن بسته به شرکت فرستنده برای فرستنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS رسیدن بسته به شرکت فرستنده برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsPackageInCompanyReceiver)
            .NotEmpty().WithMessage("الگوی SMS رسیدن بسته به شرکت فرستنده برای گیرنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS رسیدن بسته به شرکت فرستنده برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsManifestSender)
            .NotEmpty().WithMessage("الگوی SMS مانیفست برای فرستنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS مانیفست برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsManifestReceiver)
            .NotEmpty().WithMessage("الگوی SMS مانیفست برای گیرنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS مانیفست برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsReceivedInReceiverCompanySender)
            .NotEmpty().WithMessage("الگوی SMS دریافت در شرکت گیرنده برای فرستنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS دریافت در شرکت گیرنده برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsReceivedInReceiverCompanyReceiver)
            .NotEmpty().WithMessage("الگوی SMS دریافت در شرکت گیرنده برای گیرنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS دریافت در شرکت گیرنده برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsSendReceiverPeakSender)
            .NotEmpty().WithMessage("الگوی SMS ارسال پیک شرکت گیرنده برای فرستنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS ارسال پیک شرکت گیرنده برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsSendReceiverPeakReceiver)
            .NotEmpty().WithMessage("الگوی SMS ارسال پیک شرکت گیرنده برای گیرنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS ارسال پیک شرکت گیرنده برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsDeliverSender)
            .NotEmpty().WithMessage("الگوی SMS تحویل برای فرستنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS تحویل برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsDeliverReceiver)
            .NotEmpty().WithMessage("الگوی SMS تحویل برای گیرنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS تحویل برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsCancelSender)
            .NotEmpty().WithMessage("الگوی SMS لغو برای فرستنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS لغو برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsCancelReceiver)
            .NotEmpty().WithMessage("الگوی SMS لغو برای گیرنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS لغو برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsCancelByCustomerSender)
            .NotEmpty().WithMessage("الگوی SMS لغو توسط مشتری برای فرستنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS لغو توسط مشتری برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsCancelByCustomerReceiver)
            .NotEmpty().WithMessage("الگوی SMS لغو توسط مشتری برای گیرنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS لغو توسط مشتری برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsCancelByCustomerCompany)
            .NotEmpty().WithMessage("الگوی SMS لغو توسط مشتری برای شرکت الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS لغو توسط مشتری برای شرکت نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsSendManifestReceiverCompany)
            .NotEmpty().WithMessage("الگوی SMS ارسال مانیفست برای شرکت گیرنده الزامی است.")
            .MaximumLength(500).WithMessage("الگوی SMS ارسال مانیفست برای شرکت گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}