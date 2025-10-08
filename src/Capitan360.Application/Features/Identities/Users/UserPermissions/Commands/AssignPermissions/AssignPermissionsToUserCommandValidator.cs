using FluentValidation;

namespace Capitan360.Application.Features.Identities.Users.UserPermissions.Commands.AssignPermissions;

public class AssignPermissionsToUserCommandValidator : AbstractValidator<AssignPermissionsToUserCommand>
{
    public AssignPermissionsToUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("شناسه کاربر الزامی است")
            .MaximumLength(450).WithMessage("حداکثر طول شناسه کاربری 450 کاراکتر است.");

        RuleFor(x => x.PermissionList)
            .NotNull().WithMessage("لیست مجوزها نمی تواند خالی باشد");

        RuleForEach(x => x.PermissionList)
            .GreaterThan(0).WithMessage("شناسه های مربوط به مجوزها الزامی است");
    }
}