using Capitan360.Application.Features.Identities.Users.Users.Commands.UpdateByAdmin;
using FluentValidation;

namespace Capitan360.Application.Features.Identities.Users.Users.Commands.UpdateCridite;

public class UpdateCreditUserCommandValidator : AbstractValidator<UpdateCreditUserCommand>
{
    public UpdateCreditUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("شناسه کاربر الزامی است")
            .MaximumLength(450).WithMessage("شناسه کاربر نمی‌تواند بیشتر از 450 کاراکتر باشد");

        RuleFor(x => x.Credit)
            .GreaterThanOrEqualTo(0).WithMessage("میزان اعتبار باید بزرگتر یا برابر 0 باشد");
    }
}
