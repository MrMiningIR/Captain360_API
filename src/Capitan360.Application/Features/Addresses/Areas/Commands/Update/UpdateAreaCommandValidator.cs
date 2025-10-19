using FluentValidation;

namespace Capitan360.Application.Features.Addresses.Areas.Commands.Update;

public class UpdateAreaCommandValidator : AbstractValidator<UpdateAreaCommand>
{
    public UpdateAreaCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.PersianName)
            .NotEmpty().WithMessage("نام فارسی الزامی است.")
            .MaximumLength(50).WithMessage("حداکثر طول نام فارسی 50 کاراکتر است.");

        RuleFor(x => x.EnglishName)
            .NotNull().WithMessage("نام انگلیسی نمی تواند خالی باشد است.")
            .MaximumLength(50).WithMessage("حداکثر طول نام انگلیسی 50 کاراکتر است.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("کد الزامی است.")
            .MaximumLength(20).WithMessage("حداکثر طول کد 20 کاراکتر است.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180m, 180m).WithMessage("طول جغرافیایی معتبر نیست.");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-180m, 180m).WithMessage("عرض جغرافیایی معتبر نیست.");
    }
}