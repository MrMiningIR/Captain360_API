using FluentValidation;

namespace Capitan360.Application.Features.Companies.Companies.Commands.Create;

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

        RuleFor(x => x.MobileCounter)
            .NotEmpty()
            .WithMessage("شماره تلفن شرکت اجباری است")
            .Matches(@"/(^(0?9)|(\+?989))((14)|(13)|(12)|(19)|(18)|(17)|(15)|(16)|(11)|(10)|(90)|(91)|(92)|(93)|(94)|(95)|(96)|(32)|(30)|(33)|(35)|(36)|(37)|(38)|(39)|(00)|(01)|(02)|(03)|(04)|(05)|(41)|(20)|(21)|(22)|(23)|(31)|(34)|(9910)|(9911)|(9913)|(9914)|(9999)|(999)|(990)|(9810)|(9811)|(9812)|(9813)|(9814)|(9815)|(9816)|(9817)|(998))\d{7}/g")
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
            .When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("توضحیحات نباید بیشتر از 500 کاراکتر باشد");
    }
}