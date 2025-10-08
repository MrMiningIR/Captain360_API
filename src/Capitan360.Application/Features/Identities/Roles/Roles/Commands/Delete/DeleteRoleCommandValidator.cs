using FluentValidation;

namespace Capitan360.Application.Features.Identities.Roles.Roles.Commands.Delete;

public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("شناسه نقش الزامی است")
            .MaximumLength(450).WithMessage("شناسه نقش نمی‌تواند بیشتر از 450 کاراکتر باشد");
    }
}
