using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.Update;

public class UpdateCompanyManifestFormCommandValidator : AbstractValidator<UpdateCompanyManifestFormCommand>
{
    public UpdateCompanyManifestFormCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شماره شناسایی فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanySenderDescription)
           .NotNull().WithMessage("توضیحات شرکت فرستنده نمی تواند خالی باشد است.")
           .MaximumLength(500).WithMessage("توضیحات شرکت فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.CompanySenderDescriptionForPrint)
           .NotNull().WithMessage("توضیحات چاپ شرکت فرستنده نمی تواند خالی باشد است.")
           .MaximumLength(500).WithMessage("توضیحات چاپ شرکت فرستنده نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}