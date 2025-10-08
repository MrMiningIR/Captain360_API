using FluentValidation;

namespace Capitan360.Application.Features.Companies.Companies.Commands.Update;


public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه شرکت باید مشخص باشد");

        RuleFor(x => x.MobileCounter)
            .NotEmpty().WithMessage("شماره تلفن شرکت اجباری است")
            .Length(11).WithMessage("طول تلفن شرکت باید دقیقاً 11 رقم باشد.")
            .Matches(@"(^(0?9)|(\+?989))\d{2}\W?\d{3}\W?\d{4}").WithMessage("شماره تلفن شرکت معتبر نیست");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام شرکت الزامی است.")
            .MaximumLength(50).WithMessage("حداکثر طول نام 50 کاراکتر است.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("توضحیحات نباید بیشتر از 500 کاراکتر باشد");
    }
}
