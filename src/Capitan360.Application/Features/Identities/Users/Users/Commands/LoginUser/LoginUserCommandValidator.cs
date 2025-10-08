using Capitan360.Application.Features.Identities.Users.Users.Commands.LoginManager;
using FluentValidation;

namespace Capitan360.Application.Features.Identities.Users.Users.Commands.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.Mobile)
            .NotEmpty().WithMessage("شماره موبایل اجباری است")
            .Length(11).WithMessage("طول شماره موبایل باید دقیقاً 11 رقم باشد.")
            .Matches(@"(^(0?9)|(\+?989))\d{2}\W?\d{3}\W?\d{4}").WithMessage("شماره موبایل معتبر نیست");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("رمز عبور الزامی است")
            .MinimumLength(10).WithMessage("حداقل طول رمز عبور 10 کارکتر است")
            .Custom((pwd, ctx) =>
            {
                if (!pwd.Any(char.IsUpper)) ctx.AddFailure("رمز عبور باید حداقل یک حرف بزرگ انگلیسی داشته باشد.");
                if (!pwd.Any(char.IsLower)) ctx.AddFailure("رمز عبور باید حداقل یک حرف کوچک انگلیسی داشته باشد.");
                if (!pwd.Any(char.IsDigit)) ctx.AddFailure("رمز عبور باید حداقل یک عدد داشته باشد.");
                if (!pwd.Any(ch => !char.IsLetterOrDigit(ch)))
                    ctx.AddFailure("رمز عبور باید حداقل یک حرف خاص (مانند !@#$...) داشته باشد.");
            });
    }
}
