using Capitan360.Application.Extensions;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.UpdateFromDesktop;

public class UpdateCompanyManifestFormFromDesktopCommandValidator : AbstractValidator<UpdateCompanyManifestFormFromDesktopCommand>
{
    public UpdateCompanyManifestFormFromDesktopCommandValidator()
    {
        RuleFor(x => x.No)
            .GreaterThan(0).WithMessage("شماره فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanySenderCaptain360Code)
            .NotNull().WithMessage("کد کاپیتان 360 شرکت فرستنده  نمی‌تواند خالی باشد")
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت فرستنده الزامی است")
            .MaximumLength(10).WithMessage("کد کاپیتان 360 شرکت فرستنده نمی‌تواند بیشتر از 10 کاراکتر باشد");

        RuleFor(x => x.CompanySenderDescription)
           .NotNull().WithMessage("توضیحات شرکت فرستنده نمی تواند خالی باشد است.")
           .MaximumLength(500).WithMessage("توضیحات شرکت فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.CompanySenderDescriptionForPrint)
           .NotNull().WithMessage("توضیحات چاپ شرکت فرستنده نمی تواند خالی باشد است.")
           .MaximumLength(500).WithMessage("توضیحات چاپ شرکت فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.DateUpdate)
            .IsValidPersianDate("تاریخ به روز رسانی");

        RuleFor(x => x.TimeUpdate)
            .IsValidTime("ساعت به روز رسانی");

    }
}
