using FluentValidation;

namespace Capitan360.Application.Features.Identities.Roles.Roles.Commands.Create;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام نقش اجباری است.")
            .MaximumLength(256).WithMessage("حداکثر نام نقش 256 کاراکتر است.");

        RuleFor(x => x.PersianName)
            .NotEmpty().WithMessage("نام فارسی نقش اجباری است.")
            .MaximumLength(50).WithMessage("حداکثر نام نقش 50 کاراکتر است.");
    }
}
