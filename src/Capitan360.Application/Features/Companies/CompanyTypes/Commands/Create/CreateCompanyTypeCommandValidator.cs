using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyTypes.Commands.Create;

public class CreateCompanyTypeCommandValidator : AbstractValidator<CreateCompanyTypeCommand>
{
    public CreateCompanyTypeCommandValidator()
    {
        RuleFor(x => x.TypeName)
            .NotEmpty().WithMessage("نام الزامی است.")
            .MaximumLength(30).WithMessage("حداکثر طول نام برابر 30 است.");

        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("نام نمایشی الزامی است.")
            .MaximumLength(30).WithMessage("حداکثر طول نام نمایشی برابر 30 است.");

        RuleFor(x => x.Description)
            .NotNull().WithMessage("توضیحات نمی تواند خالی باشد است.")
            .MaximumLength(500).WithMessage("حداکثر طول توضیحات برابر 500 است.");
    }
}