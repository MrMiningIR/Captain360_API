using Capitan360.Application.Extensions;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.AttachToManifestFormFromDesktop;

public class AttachToCompanyManifestFormFromDesktopCommandValidator : AbstractValidator<AttachToCompanyManifestFormFromDesktopCommand>
{
    public AttachToCompanyManifestFormFromDesktopCommandValidator()
    {
        RuleFor(x => x.No)
            .GreaterThan(0).WithMessage("شماره بارنامه باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanyManifestFormNo)
            .GreaterThan(0).WithMessage("شماره فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanySenderCaptain360Code)
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت فرستنده الزامی است")
            .MinimumLength(1).WithMessage("کد کاپیتان 360 شرکت فرستنده نمی‌تواند کمتر از 1 کاراکتر باشد");

        RuleFor(x => x.DateManifested)
            .IsValidPersianDate("تاریخ مانیفست");

        RuleFor(x => x.TimeManifested)
            .IsValidTime("ساعت مانیفست");
    }
}

