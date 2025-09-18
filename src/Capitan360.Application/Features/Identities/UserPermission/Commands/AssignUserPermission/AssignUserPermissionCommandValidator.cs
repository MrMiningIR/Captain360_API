using FluentValidation;

namespace Capitan360.Application.Features.UserPermission.Commands.AssignUserPermission;

public class AssignUserPermissionCommandValidator : AbstractValidator<AssignUserPermissionCommand>
{
    public AssignUserPermissionCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("شناسه کاربری نمیتواند خالی باشد");
        RuleFor(x => x.PermissionId)
            .GreaterThan(0)
            .WithMessage("شناسه دسترسی نمیتواند 0 باشد");
    }
}