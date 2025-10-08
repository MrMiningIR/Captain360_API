using FluentValidation;

namespace Capitan360.Application.Features.Identities.Permissions.Commands.UpdateActiveState;

public class UpdateActiveStatePermissionCommandValidator : AbstractValidator<UpdateActiveStatePermissionCommand>
{
    public UpdateActiveStatePermissionCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه مجوز باید بزرگ‌تر از صفر باشد");
    }
}
