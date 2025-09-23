using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.BackFromAirlineDeliveryStateFromDesktop;

public class BackFromAirlineDeliveryStateFromDesktopCommandValidator : AbstractValidator<BackFromAirlineDeliveryStateFromDesktopCommand>
{
    public BackFromAirlineDeliveryStateFromDesktopCommandValidator()
    {
        RuleFor(x => x.No)
         .GreaterThan(0).WithMessage("شماره فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanySenderCaptain360Code)
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت فرستنده الزامی است")
            .MinimumLength(1).WithMessage("کد کاپیتان 360 شرکت فرستنده نمی‌تواند کمتر از 1 کاراکتر باشد");
    }
}