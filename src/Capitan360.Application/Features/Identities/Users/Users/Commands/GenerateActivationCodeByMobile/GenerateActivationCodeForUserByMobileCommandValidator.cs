using Capitan360.Application.Features.Identities.Users.Users.Commands.Register;
using FluentValidation;

namespace Capitan360.Application.Features.Identities.Users.Users.Commands.GenerateActivationCodeByMobile;

public class GenerateActivationCodeForUserByMobileCommandValidator : AbstractValidator<GenerateActivationCodeForUserByMobileCommand>
{
    public GenerateActivationCodeForUserByMobileCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("شناسه نقش کاربر باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.Mobile)
            .NotEmpty().WithMessage("شماره موبایل اجباری است")
            .Length(11).WithMessage("طول شماره موبایل باید دقیقاً 11 رقم باشد.")
            .Matches(@"(^(0?9)|(\+?989))\d{2}\W?\d{3}\W?\d{4}").WithMessage("شماره موبایل معتبر نیست");
    }
}
