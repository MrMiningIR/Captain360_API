using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.AttachedToIssuedManifestForm;

public class AttachedToIssuedCompanyManifestFormCommandValidator : AbstractValidator<AttachedToIssuedCompanyManifestFormCommand>
{
    public AttachedToIssuedCompanyManifestFormCommandValidator()
    {
        RuleFor(x => x.CompanyDomesticWaybillId)
         .GreaterThan(0).WithMessage("شماره شناسایی بارنامه باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanyManifestFormId)
         .GreaterThan(0).WithMessage("شماره شناسایی فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanyReceiverCaptain360Code)
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت گیرنده الزامی است")
            .MinimumLength(1).WithMessage("کد کاپیتان 360 شرکت گیرنده نمی‌تواند کمتر از 1 کاراکتر باشد");
    }
}
