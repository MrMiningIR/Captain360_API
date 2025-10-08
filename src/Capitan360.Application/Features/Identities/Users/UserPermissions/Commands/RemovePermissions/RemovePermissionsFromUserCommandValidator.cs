using FluentValidation;

namespace Capitan360.Application.Features.Identities.Users.UserPermissions.Commands.RemovePermissions;

public class RemovePermissionsFromUserCommandValidator : AbstractValidator<RemovePermissionsFromUserCommand>
{
    public RemovePermissionsFromUserCommandValidator()
    {
        RuleFor(x => x.UserPermissionIdList)
            .NotNull().WithMessage("لیست مجوزها نمی تواند خالی باشد");

        RuleForEach(x => x.UserPermissionIdList)
            .GreaterThan(0).WithMessage("شناسه های مربوط به مجوزها الزامی است");
    }
}
