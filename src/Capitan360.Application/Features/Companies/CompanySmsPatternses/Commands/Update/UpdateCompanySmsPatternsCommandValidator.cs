using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanySmsPatternses.Commands.Update;

public class UpdateCompanySmsPatternsCommandValidator : AbstractValidator<UpdateCompanySmsPatternsCommand>
{
    public UpdateCompanySmsPatternsCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه الگوهای SMS باید مشخص باشد");

        RuleFor(x => x.SmsPanelUserName)
            .NotNull().WithMessage("نام کاربری پنل SMS نمی تواند خالی باشد.")
            .MaximumLength(100).WithMessage("نام کاربری پنل SMS نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.SmsPanelPassword)
            .NotNull().WithMessage("رمز عبور پنل SMS نمی تواند خالی باشد.")
            .MaximumLength(100).WithMessage("رمز عبور پنل SMS نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.SmsPanelNumber)
            .NotNull().WithMessage("شماره پنل SMS نمی تواند خالی باشد.")
            .MaximumLength(100).WithMessage("شماره پنل SMS نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.PatternSmsIssueSender)
            .NotNull().WithMessage("الگوی SMS صدور برای فرستنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS صدور برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsIssueReceiver)
            .NotNull().WithMessage("الگوی SMS صدور برای گیرنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS صدور برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsIssueCompany)
            .NotNull().WithMessage("الگوی SMS صدور برای شرکت فرستنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS صدور برای شرکت فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsSendSenderPeakSender)
            .NotNull().WithMessage("الگوی SMS ارسال پیک شرکت فرستنده برای فرستنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS ارسال پیک شرکت فرستنده برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsSendSenderPeakReceiver)
            .NotNull().WithMessage("الگوی SMS ارسال پیک شرکت فرستنده برای گیرنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS ارسال پیک شرکت فرستنده برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsPackageInCompanySender)
            .NotNull().WithMessage("الگوی SMS رسیدن مرسوله به شرکت فرستنده برای فرستنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS رسیدن مرسوله به شرکت فرستنده برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsPackageInCompanyReceiver)
            .NotNull().WithMessage("الگوی SMS رسیدن مرسوله به شرکت فرستنده برای گیرنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS رسیدن مرسوله به شرکت فرستنده برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsManifestSender)
            .NotNull().WithMessage("الگوی SMS مانیفست بارنامه برای فرستنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS مانیفست بارنامه برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsManifestReceiver)
            .NotNull().WithMessage("الگوی SMS مانیفست بارنامه برای گیرنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS مانیفست بارنامه برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsReceivedInReceiverCompanySender)
            .NotNull().WithMessage("الگوی SMS دریافت بارنامه در شرکت گیرنده برای فرستنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS دریافت بارنامه در شرکت گیرنده برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsReceivedInReceiverCompanyReceiver)
            .NotNull().WithMessage("الگوی SMS دریافت بارنامه در شرکت گیرنده برای گیرنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS دریافت بارنامه در شرکت گیرنده برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsSendReceiverPeakSender)
            .NotNull().WithMessage("الگوی SMS ارسال پیک شرکت گیرنده برای فرستنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS ارسال پیک شرکت گیرنده برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsSendReceiverPeakReceiver)
            .NotNull().WithMessage("الگوی SMS ارسال پیک شرکت گیرنده برای گیرنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS ارسال پیک شرکت گیرنده برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsDeliverSender)
            .NotNull().WithMessage("الگوی SMS تحویل مرسوله برای فرستنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS تحویل مرسوله برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsDeliverReceiver)
            .NotNull().WithMessage("الگوی SMS تحویل مرسوله برای گیرنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS تحویل مرسوله برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsCancelSender)
            .NotNull().WithMessage("الگوی SMS لغو بارنامه برای فرستنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS لغو بارنامه برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsCancelReceiver)
            .NotNull().WithMessage("الگوی SMS لغو بارنامه برای گیرنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS لغو بارنامه برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsCancelByCustomerSender)
            .NotNull().WithMessage("الگوی SMS لغو توسط مشتری برای فرستنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS لغو توسط مشتری برای فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsCancelByCustomerReceiver)
            .NotNull().WithMessage("الگوی SMS لغو بارنامه توسط مشتری برای گیرنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS لغو بارنامه توسط مشتری برای گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsCancelByCustomerCompany)
            .NotNull().WithMessage("الگوی SMS لغو بارنامه توسط مشتری برای شرکت فرستنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS لغو بارنامه توسط مشتری برای شرکت فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.PatternSmsSendManifestReceiverCompany)
            .NotNull().WithMessage("الگوی SMS ارسال مانیفست برای شرکت گیرنده نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("الگوی SMS ارسال مانیفست برای شرکت گیرنده نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}