using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.UpdateManiFestForm;

public class UpdateManiFestFormCommandValidator : AbstractValidator<UpdateManiFestFormCommand>
{
    public UpdateManiFestFormCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شماره شناسایی فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanySenderDescription)
            .MaximumLength(1500)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanySenderDescription))
            .WithMessage("توضیحات مانبفست نمی‌تواند بیشتر از 1500 کاراکتر باشد");

        RuleFor(x => x.CompanySenderDescriptionForPrint)
            .MaximumLength(1500)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanySenderDescriptionForPrint))
            .WithMessage("توضیحات مانبفست برای چاپ نمی‌تواند بیشتر از 1500 کاراکتر باشد");

    }
}