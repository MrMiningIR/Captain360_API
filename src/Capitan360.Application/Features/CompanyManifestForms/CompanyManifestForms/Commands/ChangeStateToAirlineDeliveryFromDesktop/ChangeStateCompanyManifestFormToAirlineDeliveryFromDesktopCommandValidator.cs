using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.ChangeStateToAirlineDeliveryFromDesktop;

public class ChangeStateCompanyManifestFormToAirlineDeliveryFromDesktopCommandValidator : AbstractValidator<ChangeStateCompanyManifestFormToAirlineDeliveryFromDesktopCommand>
{
    public ChangeStateCompanyManifestFormToAirlineDeliveryFromDesktopCommandValidator()
    {
        RuleFor(x => x.No)
         .GreaterThan(0).WithMessage("شماره فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanySenderCaptain360Code)
            .NotNull().WithMessage("کد کاپیتان 360 شرکت فرستنده  نمی‌تواند خالی باشد")
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت فرستنده الزامی است")
            .MaximumLength(10).WithMessage("کد کاپیتان 360 شرکت فرستنده نمی‌تواند بیشتر از 10 کاراکتر باشد");
    }
}