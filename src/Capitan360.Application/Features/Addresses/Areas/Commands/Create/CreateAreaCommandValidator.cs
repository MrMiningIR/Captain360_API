using FluentValidation;

namespace Capitan360.Application.Features.Addresses.Areas.Commands.Create;

public class CreateAreaCommandValidator : AbstractValidator<CreateAreaCommand>
{
    public CreateAreaCommandValidator()
    {
        RuleFor(x => x.ParentId)
            .Must(pid => pid is null || pid > 0).WithMessage("والد یا باید خالی باشد یا بزرگتر از صفر");

        RuleFor(x => (int)x.LevelId)
            .NotEmpty().WithMessage("نوع الزامی است.")
            .GreaterThan(0).WithMessage("نوع باید بزرگتر از صفر باشد");

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