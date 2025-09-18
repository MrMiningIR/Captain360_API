using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyType.Commands.CreateCompanyType;

public class CreateCompanyTypeCommandValidator : AbstractValidator<CreateCompanyTypeCommand>
{
    public CreateCompanyTypeCommandValidator()
    {
        RuleFor(x => x.TypeName)
            .NotEmpty().WithMessage("نام نوع شرکت الزامی است")
            .MaximumLength(100).WithMessage("نام نوع شرکت نمی‌تواند بیشتر از 100 کاراکتر باشد")
            .MinimumLength(4).WithMessage("نام نوع شرکت نمی‌تواند کمتر از 4 کاراکتر باشد");

        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("نام نمایشی الزامی است")
            .MaximumLength(100).WithMessage("نام نمایشی نمی‌تواند بیشتر از 100 کاراکتر باشد")
            .MinimumLength(4).WithMessage("نام نمایشی نمی‌تواند کمتر از 4 کاراکتر باشد");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("توضیحات نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}