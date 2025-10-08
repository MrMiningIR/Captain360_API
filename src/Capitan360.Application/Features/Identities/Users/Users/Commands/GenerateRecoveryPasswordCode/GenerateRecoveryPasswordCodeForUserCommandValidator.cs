using FluentValidation;

namespace Capitan360.Application.Features.Identities.Users.Users.Commands.GenerateRecoveryPasswordCode;

public class GenerateRecoveryPasswordCodeForUserCommandValidator : AbstractValidator<GenerateRecoveryPasswordCodeForUserCommand>
{
    public GenerateRecoveryPasswordCodeForUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("شناسه کاربر الزامی است")
            .MaximumLength(450).WithMessage("شناسه کاربر نمی‌تواند بیشتر از 450 کاراکتر باشد");

        RuleFor(x => x.Mobile)
            .NotEmpty().WithMessage("شماره موبایل اجباری است")
            .Length(11).WithMessage("طول شماره موبایل باید دقیقاً 11 رقم باشد.")
            .Matches(@"(^(0?9)|(\+?989))\d{2}\W?\d{3}\W?\d{4}").WithMessage("شماره موبایل معتبر نیست");
    }
}
