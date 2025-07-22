using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyType.Commands.CreateCompanyType;

public class CreateCompanyTypeValidator : AbstractValidator<CreateCompanyTypeCommand>
{
    public CreateCompanyTypeValidator()
    {
        RuleFor(x => x.TypeName)
            .NotEmpty().WithMessage("نام نوع شرکت الزامی است")
            .MaximumLength(50).WithMessage("نام نوع شرکت نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("نام نمایشی الزامی است")
            .MaximumLength(100).WithMessage("نام نمایشی نمی‌تواند بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.Description)
            .MaximumLength(150).WithMessage("توضیحات نمی‌تواند بیشتر از 150 کاراکتر باشد")
            .When(x => x.Description != null);
    }
}