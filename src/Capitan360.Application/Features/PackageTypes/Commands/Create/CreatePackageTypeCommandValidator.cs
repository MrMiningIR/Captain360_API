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
            .MinimumLength(4).WithMessage("حداقل طول نام 4 کاراکتر است.")
            .MaximumLength(30).WithMessage("حداکثر طول نام 30 کاراکتر است.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("حداکثر طول توضیحات 500 کاراکتر است.");
    }
}