using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.AttachedToIssuedManifestFormFromDesktop;

public class AttachedToIssuedComapnyManifestFormFromDesktopCommandValidator : AbstractValidator<AttachedToIssuedCompanyManifestFormFromDesktopCommand>
{
    public AttachedToIssuedComapnyManifestFormFromDesktopCommandValidator()
    {
        RuleFor(x => x.CompanyDomesticWaybillNo)
            .NotEmpty().WithMessage("شماره بارنامه الزامی است");

        RuleFor(x => x.CompanyManifestFormNo)
         .GreaterThan(0).WithMessage("شماره فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanyReceiverCaptain360Code)
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت گیرنده الزامی است")
            .MinimumLength(1).WithMessage("کد کاپیتان 360 شرکت گیرنده نمی‌تواند کمتر از 1 کاراکتر باشد");
    }
}
