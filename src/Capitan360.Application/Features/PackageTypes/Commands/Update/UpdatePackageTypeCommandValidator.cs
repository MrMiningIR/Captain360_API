using FluentValidation;

namespace Capitan360.Application.Features.PackageTypes.Commands.Update;

public class UpdatePackageTypeCommandValidator : AbstractValidator<UpdatePackageTypeCommand>
{
    public UpdatePackageTypeCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگتر از صفر باشد");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام الزامی است.")
            .MaximumLength(30).WithMessage("حداکثر طول نام 30 کاراکتر است.");

        RuleFor(x => x.Description)
            .NotNull().WithMessage("توضیحات نمی تواند خالی باشد است.")
            .MaximumLength(500).WithMessage("حداکثر طول توضیحات 500 کاراکتر است.");
    }
}