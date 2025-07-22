using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyType.Commands.UpdateCompanyType;

public class UpdateCompanyTypeCommandValidator : AbstractValidator<UpdateCompanyTypeCommand>
{
    public UpdateCompanyTypeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت باید مشخص باشد");

        RuleFor(x => x.TypeName)
            .NotEmpty().WithMessage("نام نوع شرکت نمی‌تواند خالی باشد")
            .MaximumLength(50).WithMessage("نام نوع شرکت نمی‌تواند بیشتر از 50 کاراکتر باشد")
            .When(x => x.TypeName != null);

        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("نام نمایشی نمی‌تواند خالی باشد")
            .MaximumLength(100).WithMessage("نام نمایشی نمی‌تواند بیشتر از 100 کاراکتر باشد")
            .When(x => x.DisplayName != null);

        RuleFor(x => x.Description)
            .MaximumLength(150).WithMessage("توضیحات نمی‌تواند بیشتر از 150 کاراکتر باشد")
            .When(x => x.Description != null);
    }
}