using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyTypes.Commands.Update;

public class UpdateCompanyTypeCommandValidator : AbstractValidator<UpdateCompanyTypeCommand>
{
    public UpdateCompanyTypeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت باید مشخص باشد");

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