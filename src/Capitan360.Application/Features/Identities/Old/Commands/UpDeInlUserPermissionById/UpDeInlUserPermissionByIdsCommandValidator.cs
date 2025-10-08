using FluentValidation;

namespace Capitan360.Application.Features.Identities.Old.Commands.UpDeInlUserPermissionById;

public class UpDeInlUserPermissionByIdsCommandValidator : AbstractValidator<UpDeInlUserPermissionByIdsCommand>
{
    public UpDeInlUserPermissionByIdsCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage("شناسنه کاربر نمیتوالند خالی باشد");
    }
}