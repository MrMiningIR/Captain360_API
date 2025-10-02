using Capitan360.Application.Extensions;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.IssueFromDesktop;

public class IssueCompanyManifestFormFromDesktopCommandValidator : AbstractValidator<IssueCompanyManifestFormFromDesktopCommand>
{
    public IssueCompanyManifestFormFromDesktopCommandValidator()
    {
        RuleFor(x => x.No)
            .GreaterThan(0).WithMessage("شماره فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanySenderCaptain360Code)
            .NotNull().WithMessage("کد کاپیتان 360 شرکت فرستنده  نمی‌تواند خالی باشد")
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت فرستنده الزامی است")
            .MaximumLength(10).WithMessage("کد کاپیتان 360 شرکت فرستنده نمی‌تواند بیشتر از 10 کاراکتر باشد");

        RuleFor(x => x.CompanyReceiverUserInsertedCode)
            .NotNull().WithMessage("کد کاپیتان 360 شرکت گیرنده  نمی‌تواند خالی باشد")
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت گیرنده الزامی است")
            .MaximumLength(10).WithMessage("کد کاپیتان 360 شرکت گیرنده نمی‌تواند بیشتر از 10 کاراکتر باشد");

        RuleFor(x => x.Date)
                .IsValidPersianDate("تاریخ مانیفست");

        RuleFor(x => x.CompanySenderDescription)
           .NotNull().WithMessage("توضیحات شرکت فرستنده نمی تواند خالی باشد است.")
           .MaximumLength(500).WithMessage("توضیحات شرکت فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.CompanySenderDescriptionForPrint)
           .NotNull().WithMessage("توضیحات چاپ شرکت فرستنده نمی تواند خالی باشد است.")
           .MaximumLength(500).WithMessage("توضیحات چاپ شرکت فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.DateIssued)
            .IsValidPersianDate("تاریخ صدور");

        RuleFor(x => x.TimeIssued)
            .IsValidTime("ساعت صدور");
    }
}