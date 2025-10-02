using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFromCompanyManifestFormFromDesktop;

public class BackCompanyDomesticWaybillFromCompanyManifestFormFromDesktopCommandValidator : AbstractValidator<BackCompanyDomesticWaybillFromCompanyManifestFormFromDesktopCommand>
{
    public BackCompanyDomesticWaybillFromCompanyManifestFormFromDesktopCommandValidator()
    {
        RuleFor(x => x.No)
         .GreaterThan(0).WithMessage("شماره فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanySenderCaptain360Code)
            .NotNull().WithMessage("کد کاپیتان 360 شرکت فرستنده  نمی‌تواند خالی باشد")
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت فرستنده الزامی است")
            .MaximumLength(10).WithMessage("کد کاپیتان 360 شرکت فرستنده نمی‌تواند بیشتر از 10 کاراکتر باشد");
    }
}
