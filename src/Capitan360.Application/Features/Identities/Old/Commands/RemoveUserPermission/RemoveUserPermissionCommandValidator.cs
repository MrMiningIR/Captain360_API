using FluentValidation;

namespace Capitan360.Application.Features.Identities.Old.Commands.RemoveUserPermission;

public class RemoveUserPermissionCommandValidator : AbstractValidator<RemoveUserPermissionCommand>
{
    public RemoveUserPermissionCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("شناسه کاربری نمیتواند خالی باشد");
        RuleFor(x => x.PermissionId)
            .GreaterThan(0)
            .WithMessage("شناسه دسترسی نمیتواند 0 باشد");
    }
}