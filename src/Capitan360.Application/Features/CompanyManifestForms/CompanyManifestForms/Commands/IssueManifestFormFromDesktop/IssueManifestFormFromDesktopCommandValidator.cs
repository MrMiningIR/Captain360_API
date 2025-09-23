using Capitan360.Application.Extensions;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.IssueManifestFormFromDesktop;

public class IssueManifestFormFromDesktopCommandValidator : AbstractValidator<IssueManifestFormFromDesktopCommand>
{
    public IssueManifestFormFromDesktopCommandValidator()
    {
        RuleFor(x => x.No)
            .GreaterThan(0).WithMessage("شماره فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanySenderCaptain360Code)
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت فرستنده الزامی است")
            .MinimumLength(1).WithMessage("کد کاپیتان 360 شرکت فرستنده نمی‌تواند کمتر از 1 کاراکتر باشد");

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

        RuleFor(x => x.DateIssued)
            .IsValidPersianDate("تاریخ صدور");

        RuleFor(x => x.TimeIssued)
            .IsValidTime("ساعت صدور");
    }
}