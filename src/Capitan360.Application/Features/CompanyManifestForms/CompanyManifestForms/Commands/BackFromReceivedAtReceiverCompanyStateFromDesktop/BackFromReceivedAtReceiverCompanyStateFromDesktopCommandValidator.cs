using Capitan360.Application.Extensions;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.BackFromReceivedAtReceiverCompanyStateFromDesktop;

public class BackFromReceivedAtReceiverCompanyStateFromDesktopCommandValidator : AbstractValidator<BackCompanyManifestFormFromReceivedAtReceiverCompanyStateFromDesktopCommand>
{
    public BackFromReceivedAtReceiverCompanyStateFromDesktopCommandValidator()
    {
        RuleFor(x => x.No)
         .GreaterThan(0).WithMessage("شماره فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanyReceiverCaptain360Code)
            .NotNull().WithMessage("کد کاپیتان 360 شرکت گیرنده  نمی‌تواند خالی باشد")
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت گیرنده الزامی است")
            .MaximumLength(10).WithMessage("کد کاپیتان 360 شرکت گیرنده نمی‌تواند بیشتر از 10 کاراکتر باشد");
    }
}