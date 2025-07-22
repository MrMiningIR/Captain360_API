using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.Company.Commands.UpdateCompany;


public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه شرکت باید مشخص باشد");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("کد شرکت نمی‌تواند خالی باشد")
            .MaximumLength(50).WithMessage("کد شرکت نمی‌تواند بیشتر از 50 کاراکتر باشد")
            .When(x => x.Code != null); // فقط اگه مقدار داره

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("شماره تلفن نمی‌تواند خالی باشد")
            .Matches(@"^\+?\d{10,15}$").WithMessage("شماره تلفن باید عددی و بین 10 تا 15 رقم باشد")
            .When(x => x.PhoneNumber != null);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام شرکت نمی‌تواند خالی باشد")
            .MaximumLength(100).WithMessage("نام شرکت نمی‌تواند بیشتر از 100 کاراکتر باشد")
            .When(x => x.Name != null);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("توضیحات نمی‌تواند خالی باشد")
            .When(x => x.Description != null);

        RuleFor(x => x.CompanyTypeId)
            .GreaterThan(0).WithMessage("نوع شرکت باید مشخص باشد")
           ;


    }
}
