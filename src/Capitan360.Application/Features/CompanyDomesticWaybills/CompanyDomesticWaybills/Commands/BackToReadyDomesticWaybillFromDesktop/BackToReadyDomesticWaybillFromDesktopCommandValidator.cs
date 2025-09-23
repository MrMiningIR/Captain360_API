using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackToReadyDomesticWaybillFromDesktop;

public class BackToReadyDomesticWaybillFromDesktopCommandValidator : AbstractValidator<BackToReadyDomesticWaybillFromDesktopCommand>
{
    public BackToReadyDomesticWaybillFromDesktopCommandValidator()
    {
        RuleFor(x => x.No)
         .GreaterThan(0).WithMessage("شماره فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanySenderCaptain360Code)
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت فرستنده الزامی است")
            .MinimumLength(1).WithMessage("کد کاپیتان 360 شرکت فرستنده نمی‌تواند کمتر از 1 کاراکتر باشد");
    }
}
