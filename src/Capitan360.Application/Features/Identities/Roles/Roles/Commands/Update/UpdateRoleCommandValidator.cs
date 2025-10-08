using FluentValidation;

namespace Capitan360.Application.Features.Identities.Roles.Roles.Commands.Update;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("شناسه نقش الزامی است")
            .MaximumLength(450).WithMessage("شناسه نقش نمی‌تواند بیشتر از 450 کاراکتر باشد");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام نقش اجباری است.")
            .MaximumLength(256).WithMessage("حداکثر نام نقش 256 کاراکتر است.");

        RuleFor(x => x.PersianName)
            .NotEmpty().WithMessage("نام فارسی نقش اجباری است.")
            .MaximumLength(50).WithMessage("حداکثر نام نقش 50 کاراکتر است.");
    }
}
