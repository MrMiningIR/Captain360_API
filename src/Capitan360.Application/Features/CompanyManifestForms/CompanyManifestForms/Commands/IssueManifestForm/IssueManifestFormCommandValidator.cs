using Capitan360.Application.Extensions;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.IssueManifestForm;

public class IssueManifestFormCommandValidator : AbstractValidator<IssueManifestFormCommand>
{
    public IssueManifestFormCommandValidator()
    {
        RuleFor(x => x.No)
            .GreaterThan(0).WithMessage("شماره فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanySenderId)
            .GreaterThan(0).WithMessage("شماره شناسایی شرکت فرستنده باید بزرگتر از صفر باشد");

        RuleFor(x => x)
            .Must(x =>
                x.CompanyReceiverId.HasValue &&
                 string.IsNullOrEmpty(x.CompanyReceiverCaptain360Code) &&
                 string.IsNullOrEmpty(x.CompanyReceiverCaptain360Name)
                ||
                !x.CompanyReceiverId.HasValue &&
                 !string.IsNullOrEmpty(x.CompanyReceiverCaptain360Code) &&
                 !string.IsNullOrEmpty(x.CompanyReceiverCaptain360Name)
            )
            .WithMessage("شرکت گیرنده معتبر نیست");

        RuleFor(x => x.CompanyReceiverCaptain360Code)
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت گیرنده الزامی است")
            .MinimumLength(1).WithMessage("کد کاپیتان 360 شرکت گیرنده نمی‌تواند کمتر از 1 کاراکتر باشد");

        RuleFor(x => x.CompanyReceiverCaptain360Name)
            .NotEmpty().WithMessage("نام کاپیتان 360 شرکت گیرنده الزامی است")
            .MinimumLength(1).WithMessage("نام کاپیتان 360 شرکت گیرنده نمی‌تواند کمتر از 1 کاراکتر باشد");

        RuleFor(x => x.Date)
                .IsValidPersianDate("تاریخ مانیفست");

        RuleFor(x => x.CompanySenderDescription)
            .MaximumLength(1500)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanySenderDescription))
            .WithMessage("توضیحات مانبفست نمی‌تواند بیشتر از 1500 کاراکتر باشد");

        RuleFor(x => x.CompanySenderDescriptionForPrint)
            .MaximumLength(1500)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanySenderDescriptionForPrint))
            .WithMessage("توضیحات مانبفست برای چاپ نمی‌تواند بیشتر از 1500 کاراکتر باشد");
    }
}