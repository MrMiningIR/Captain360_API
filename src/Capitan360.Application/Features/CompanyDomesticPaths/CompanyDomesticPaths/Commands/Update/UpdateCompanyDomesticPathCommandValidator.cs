using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Update;

public class UpdateCompanyDomesticPathCommandValidator : AbstractValidator<UpdateCompanyDomesticPathCommand>
{
    public UpdateCompanyDomesticPathCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه مسیر داخلی شرکت باید بزرگتر از صفر باشد");

        RuleFor(x => x.Description)
            .NotNull().WithMessage("توضیحات نمی تواند خالی باشد است.")
            .MaximumLength(500).WithMessage("حداکثر طول توضیحات 500 کاراکتر است.");

        RuleFor(x => x.DescriptionForSearch)
            .NotNull().WithMessage("توضیحات مسیر نمی تواند خالی باشد است.")
            .MaximumLength(500).WithMessage("حداکثر طول توضیحات مسیر 500 کاراکتر است.");
    }
}