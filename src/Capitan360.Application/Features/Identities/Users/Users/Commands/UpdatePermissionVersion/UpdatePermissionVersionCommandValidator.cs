using Capitan360.Application.Features.Identities.Users.Users.Commands.UpdateByAdmin;
using FluentValidation;

namespace Capitan360.Application.Features.Identities.Users.Users.Commands.UpdatePermissionVersion;

public class UpdatePermissionVersionCommandValidator : AbstractValidator<UpdatePermissionVersionCommand>
{
    public UpdatePermissionVersionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("شناسه کاربر الزامی است")
            .MaximumLength(450).WithMessage("شناسه کاربر نمی‌تواند بیشتر از 450 کاراکتر باشد");

        RuleFor(x => x.PermissionVersion)
            .NotNull().WithMessage("مجوز نمی تواند خالی باشد.")
            .MaximumLength(36).WithMessage("مجوز نباید بیشتر از 36 کاراکتر باشد");

    }
}
