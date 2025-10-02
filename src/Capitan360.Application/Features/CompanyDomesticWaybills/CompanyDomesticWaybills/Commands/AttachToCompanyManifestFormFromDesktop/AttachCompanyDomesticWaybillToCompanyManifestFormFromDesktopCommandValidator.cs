using Capitan360.Application.Extensions;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.AttachToCompanyManifestFormFromDesktop;

public class AttachCompanyDomesticWaybillToCompanyManifestFormFromDesktopCommandValidator : AbstractValidator<AttachCompanyDomesticWaybillToCompanyManifestFormFromDesktopCommand>
{
    public AttachCompanyDomesticWaybillToCompanyManifestFormFromDesktopCommandValidator()
    {
        RuleFor(x => x.No)
            .GreaterThan(0).WithMessage("شماره بارنامه باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanyManifestFormNo)
            .GreaterThan(0).WithMessage("شماره فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanySenderCaptain360Code)
            .NotNull().WithMessage("کد کاپیتان 360 شرکت فرستنده  نمی‌تواند خالی باشد")
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت فرستنده الزامی است")
            .MaximumLength(10).WithMessage("کد کاپیتان 360 شرکت فرستنده نمی‌تواند بیشتر از 10 کاراکتر باشد");

        RuleFor(x => x.DateManifested)
            .IsValidPersianDate("تاریخ مانیفست");

        RuleFor(x => x.TimeManifested)
            .IsValidTime("ساعت مانیفست");
    }
}

