using FluentValidation;

namespace Capitan360.Application.Features.Identities.Users.Users.Commands.GenerateActivationCodeById;

public class GenerateActivationCodeForUserByIdCommandValidator : AbstractValidator<GenerateActivationCodeForUserByIdCommand>
{
    public GenerateActivationCodeForUserByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("شناسه کاربر الزامی است")
            .MaximumLength(450).WithMessage("شناسه کاربر نمی‌تواند بیشتر از 450 کاراکتر باشد");
    }
}
