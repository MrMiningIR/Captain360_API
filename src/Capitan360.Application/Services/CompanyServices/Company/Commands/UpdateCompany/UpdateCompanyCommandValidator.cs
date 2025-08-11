using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.Company.Commands.UpdateCompany;


public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyCommandValidator()
    {
        RuleFor(x => x.Id)
     .GreaterThan(0).WithMessage("شناسه شرکت باید مشخص باشد");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("شماره تلفن شرکت اجباری است")
            .Matches(@"^\d{11}$")
            .WithMessage("شماره تلفن شرکت باید 11 کارکتر باشد");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("نام شرکت اجباری است")
            .MinimumLength(4)
            .WithMessage("کد شرکت نمی‌تواند کمتر از 4 کاراکتر باشد")
            .MaximumLength(50)
            .WithMessage("نام شرکت نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("توضحیحات نباید بیشتر از 500 کاراکتر باشد")
            .When(x => !string.IsNullOrEmpty(x.Description));




    }
}
