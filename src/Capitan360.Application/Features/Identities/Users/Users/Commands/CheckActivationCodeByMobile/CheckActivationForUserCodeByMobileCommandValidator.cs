using FluentValidation;

namespace Capitan360.Application.Features.Identities.Users.Users.Commands.CheckActivationCodeByMobile;

public class CheckActivationForUserCodeByMobileCommandValidator : AbstractValidator<CheckActivationForUserCodeByMobileCommand>
{
    public CheckActivationForUserCodeByMobileCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.Mobile)
            .NotEmpty().WithMessage("شماره موبایل اجباری است")
            .Length(11).WithMessage("طول شماره موبایل باید دقیقاً 11 رقم باشد.")
            .Matches(@"(^(0?9)|(\+?989))\d{2}\W?\d{3}\W?\d{4}").WithMessage("شماره موبایل معتبر نیست");

        RuleFor(x => x.ActivationCode)
            .NotEmpty().WithMessage("کد فعال‌سازی الزامی است.")
            .Length(6).WithMessage("کد فعال‌سازی باید ۶ کاراکتر باشد.");
    }
}
