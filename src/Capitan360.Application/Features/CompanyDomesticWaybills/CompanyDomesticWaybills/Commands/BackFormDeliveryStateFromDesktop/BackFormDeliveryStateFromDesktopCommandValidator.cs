using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFormDeliveryStateFromDesktop;

public class BackFormDeliveryStateFromDesktopCommandValidator : AbstractValidator<BackFormDeliveryStateFromDesktopCommand>
{
    public BackFormDeliveryStateFromDesktopCommandValidator()
    {
        RuleFor(x => x.No)
            .GreaterThan(0).WithMessage("شماره فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanyReceiverCaptain360Code)
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت گیرنده الزامی است")
            .MinimumLength(1).WithMessage("کد کاپیتان 360 شرکت گیرنده نمی‌تواند کمتر از 1 کاراکتر باشد");
    }
}
