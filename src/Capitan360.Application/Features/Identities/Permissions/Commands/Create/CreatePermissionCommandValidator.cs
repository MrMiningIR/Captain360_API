using FluentValidation;

namespace Capitan360.Application.Features.Identities.Permissions.Commands.Create;

public class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
{
    public CreatePermissionCommandValidator()
    {
        RuleFor(x => x.PermissionCode)
            .NotEmpty()
            .Must(g => g != Guid.Empty)
            .WithMessage("کد مجوز معتبر نیست");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام الزامی است.")
            .MaximumLength(200).WithMessage("حداکثر طول نام 200 کاراکتر است.");

        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("نام نمایشی الزامی است.")
            .MaximumLength(200).WithMessage("حداکثر طول نام نمایشی 200 کاراکتر است.");

        RuleFor(x => x.ParentCode)
            .NotEmpty()
            .Must(g => g != Guid.Empty)
            .WithMessage("کد والد مجوز معتبر نیست");

        RuleFor(x => x.ParentName)
            .NotEmpty().WithMessage("نام والد الزامی است.")
            .MaximumLength(200).WithMessage("حداکثر طول نام والد 200 کاراکتر است.");

        RuleFor(x => x.ParentDisplayName)
            .NotEmpty().WithMessage("نام نمایشی الزامی است.")
            .MaximumLength(200).WithMessage("حداکثر طول نام نمایشی والد 200 کاراکتر است.");
    }
}
