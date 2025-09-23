using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Update;

public class UpdateCompanyDomesticPathCommandValidator : AbstractValidator<UpdateCompanyDomesticPathCommand>
{
    public UpdateCompanyDomesticPathCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه مسیر داخلی شرکت باید بزرگتر از صفر باشد");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithMessage("توضیحات مسیر نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.DescriptionForSearch)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.DescriptionForSearch))
            .WithMessage("توضیحات مسیر در هنگام جستجو نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}