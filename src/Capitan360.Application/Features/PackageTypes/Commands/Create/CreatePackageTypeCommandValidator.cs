using FluentValidation;

namespace Capitan360.Application.Features.PackageTypes.Commands.Create;

public class CreatePackageTypeCommandValidator : AbstractValidator<CreatePackageTypeCommand>
{
    public CreatePackageTypeCommandValidator()
    {
        RuleFor(x => x.CompanyTypeId)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت باید بزرگتر از صفر باشد");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام الزامی است.")
            .MaximumLength(30).WithMessage("حداکثر طول نام 30 کاراکتر است.");

        RuleFor(x => x.Description)
            .NotNull().WithMessage("توضیحات نمی تواند خالی باشد است.")
            .MaximumLength(500).WithMessage("حداکثر طول توضیحات 500 کاراکتر است.");
    }
}