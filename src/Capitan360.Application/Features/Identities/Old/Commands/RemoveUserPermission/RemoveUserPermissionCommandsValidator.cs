using FluentValidation;

namespace Capitan360.Application.Features.Identities.Old.Commands.RemoveUserPermission;

public class RemoveUserPermissionCommandsValidator : AbstractValidator<RemoveUserPermissionCommands>
{
    public RemoveUserPermissionCommandsValidator()
    {
        RuleFor(x => x.PermissionList)
            .NotNull()
            .WithMessage("لیست دسترسی‌ها نمی‌تواند خالی باشد")
            .Must(list => list != null && list.Any())
            .WithMessage("لیست دسترسی‌ها باید حداقل یک آیتم داشته باشد");

        RuleFor(x => x.PermissionList)
            .Must(list => list.GroupBy(p => new { p.UserId, p.PermissionId }).All(g => g.Count() == 1))
            .WithMessage("ترکیب شناسه کاربری و دسترسی نمی‌تواند تکراری باشد");

        // For Each item in list
        RuleForEach(x => x.PermissionList)
            .SetValidator(new RemoveUserPermissionCommandValidator());
    }
}