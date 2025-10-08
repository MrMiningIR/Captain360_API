using Capitan360.Application.Features.Identities.Users.Users.Commands.CheckActivationCodeById;
using FluentValidation;

namespace Capitan360.Application.Features.Identities.Users.Users.Commands.CheckRecoveryPasswordCode;

public class CheckRecoveryPasswordCodeForUserCommandValidator : AbstractValidator<CheckRecoveryPasswordCodeForUserCommand>
{
    public CheckRecoveryPasswordCodeForUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("شناسه کاربر الزامی است")
            .MaximumLength(450).WithMessage("شناسه کاربر نمی‌تواند بیشتر از 450 کاراکتر باشد");

        RuleFor(x => x.RecoveryPasswordCode)
            .NotEmpty().WithMessage("کد بازیابی رمز الزامی است.")
            .Length(6).WithMessage("کد بازیابی رمز باید ۶ کاراکتر باشد.");
    }
}
