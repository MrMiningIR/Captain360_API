using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.Company.Commands.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("کد شرکت اجباری است")
            .MaximumLength(4)
            .WithMessage("نام شرکت نمی‌تواند بیشتر از 4 کاراکتر باشد")
            .MinimumLength(4)
            .WithMessage("کد شرکت نمی‌تواند کمتر از 4 کاراکتر باشد");

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

        RuleFor(x => x.CompanyTypeId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("نوع شرکت اجباری است");

        RuleFor(x => x.CountryId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("کشور شرکت اجباری است");

        RuleFor(x => x.ProvinceId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("استان شرکت اجباری است");

        RuleFor(x => x.CityId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("شهر شرکت اجباری است");

        RuleFor(x => x.Description)
        .MaximumLength(500)
        .WithMessage("توضحیحات نباید بیشتر از 500 کاراکتر باشد")
        .When(x => !string.IsNullOrEmpty(x.Description));

    }
}