using FluentValidation;

namespace Capitan360.Application.Features.Identities.Permissions.Commands.Delete;

public class DeletePermissionCommandValidator : AbstractValidator<DeletePermissionCommand>
{
    public DeletePermissionCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه مجوز باید بزرگ‌تر از صفر باشد");
    }
}
