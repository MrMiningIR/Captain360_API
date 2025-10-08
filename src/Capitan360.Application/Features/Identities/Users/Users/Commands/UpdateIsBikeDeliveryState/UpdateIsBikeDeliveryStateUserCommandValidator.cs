using FluentValidation;

namespace Capitan360.Application.Features.Identities.Users.Users.Commands.UpdateIsBikeDeliveryState;

public class UpdateIsBikeDeliveryStateUserCommandValidator : AbstractValidator<UpdateIsBikeDeliveryStateUserCommand>
{
    public UpdateIsBikeDeliveryStateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("شناسه کاربر الزامی است")
            .MaximumLength(450).WithMessage("شناسه کاربر نمی‌تواند بیشتر از 450 کاراکتر باشد");
    }
}
