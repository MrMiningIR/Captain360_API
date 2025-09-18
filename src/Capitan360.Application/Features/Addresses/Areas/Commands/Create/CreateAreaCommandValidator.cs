using FluentValidation;

namespace Capitan360.Application.Features.Addresses.Areas.Commands.Create;

public class CreateAreaCommandValidator : AbstractValidator<CreateAreaCommand>
{
    public CreateAreaCommandValidator()
    {
        RuleFor(x => x.PersianName)
            .NotEmpty().WithMessage("نام پارسی اجباری است")
            .MaximumLength(50).WithMessage("نام پارسی نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.EnglishName)
            .MaximumLength(50).WithMessage("نام انگلیسی نمی‌تواند بیشتر از 50 کاراکتر باشد")
            .When(x => x.EnglishName != null);

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("کد اجباری است")
            .MaximumLength(10).WithMessage("کد نمی‌تواند بیشتر از 10 کاراکتر باشد")
            .MinimumLength(2).WithMessage("کد باید حداقل 2 کاراکتر باشد");

        RuleFor(x => x.LevelId)
            .GreaterThanOrEqualTo((short)0).WithMessage("سطح باید صفر یا بیشتر باشد");

        RuleFor(x => x.ParentId)
            .GreaterThan(0).WithMessage("شناسه والد باید بزرگ‌تر از صفر باشد")
            .When(x => x.ParentId.HasValue);
    }
}