using FluentValidation;

namespace Capitan360.Application.Features.Addresses.Areas.Commands.Update;

public class UpdateAreaCommandValidator : AbstractValidator<UpdateAreaCommand>
{
    public UpdateAreaCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه منطقه باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.PersianName)
            .NotEmpty().WithMessage("نام پارسی نمی‌تواند خالی باشد")
            .MaximumLength(50).WithMessage("نام پارسی نمی‌تواند بیشتر از 50 کاراکتر باشد")
            .When(x => x.PersianName != null);

        RuleFor(x => x.EnglishName)
            .MaximumLength(50).WithMessage("نام انگلیسی نمی‌تواند بیشتر از 50 کاراکتر باشد")
            .When(x => x.EnglishName != null);

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("کد نمی‌تواند خالی باشد")
            .MaximumLength(10).WithMessage("کد نمی‌تواند بیشتر از 10 کاراکتر باشد")
            .When(x => x.Code != null);

        RuleFor(x => x.LevelId)
            .GreaterThanOrEqualTo((short)0).WithMessage("سطح باید صفر یا بیشتر باشد")
            .When(x => x.LevelId.HasValue);

        RuleFor(x => x.ParentId)
            .GreaterThan(0).WithMessage("شناسه والد باید بزرگ‌تر از صفر باشد")
            .When(x => x.ParentId.HasValue);
    }
}