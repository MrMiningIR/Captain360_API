using FluentValidation;

namespace Capitan360.Application.Features.Identities.Users.Users.Commands.CheckActivationCodeById;

public class CheckActivationForCodeUserByIdCommandValidator : AbstractValidator<CheckActivationCodeForUserByIdCommand>
{
    public CheckActivationForCodeUserByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("شناسه کاربر الزامی است")
            .MaximumLength(450).WithMessage("شناسه کاربر نمی‌تواند بیشتر از 450 کاراکتر باشد");

        RuleFor(x => x.ActivationCode)
            .NotEmpty().WithMessage("کد فعال‌سازی الزامی است.")
            .Length(6).WithMessage("کد فعال‌سازی باید ۶ کاراکتر باشد.");
    }
}
