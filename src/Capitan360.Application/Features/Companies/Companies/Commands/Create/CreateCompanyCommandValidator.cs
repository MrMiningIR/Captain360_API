using FluentValidation;

namespace Capitan360.Application.Features.Companies.Companies.Commands.Create;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("کد شرکت الزامی است.")
            .MaximumLength(4).WithMessage("نام شرکت نمی‌تواند بیشتر از 4 کاراکتر باشد")
            .MinimumLength(4).WithMessage("کد شرکت نمی‌تواند کمتر از 4 کاراکتر باشد");

        RuleFor(x => x.MobileCounter)
            .NotEmpty().WithMessage("شماره تلفن شرکت اجباری است")
            .Length(11).WithMessage("طول تلفن شرکت باید دقیقاً 11 رقم باشد.")
            .Matches(@"(^(0?9)|(\+?989))\d{2}\W?\d{3}\W?\d{4}").WithMessage("شماره تلفن شرکت معتبر نیست");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام شرکت الزامی است.")
            .MaximumLength(50).WithMessage("حداکثر طول نام 50 کاراکتر است.");

        RuleFor(x => x.CompanyTypeId)
             .GreaterThan(0).WithMessage("نوع شرکت اجباری است");

        RuleFor(x => x.CountryId)
            .GreaterThan(0).WithMessage("کشور شرکت اجباری است");

        RuleFor(x => x.ProvinceId)
            .GreaterThan(0).WithMessage("استان شرکت اجباری است");

        RuleFor(x => x.CityId)
            .GreaterThan(0).WithMessage("شهر شرکت اجباری است");

        RuleFor(x => x.Description)
            .NotNull().WithMessage("توضیحات نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("توضحیحات نباید بیشتر از 500 کاراکتر باشد");
    }
}