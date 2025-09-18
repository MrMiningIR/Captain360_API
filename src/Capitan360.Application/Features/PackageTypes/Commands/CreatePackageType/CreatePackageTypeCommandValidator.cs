using FluentValidation;

namespace Capitan360.Application.Features.PackageTypeService.Commands.CreatePackageType;

public class CreatePackageTypeCommandValidator : AbstractValidator<CreatePackageTypeCommand>
{
    public CreatePackageTypeCommandValidator()
    {
        RuleFor(x => x.CompanyTypeId)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت باید بزرگتر از صفر باشد");

        RuleFor(x => x.PackageTypeName)
            .NotEmpty().WithMessage("نام بسته بندی بار الزامی است")
            .MinimumLength(4).WithMessage("نام بسته بندی بار نمی‌تواند کمتر از 4 کاراکتر باشد")
            .MaximumLength(50).WithMessage("نام بسته بندی بار نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.PackageTypeDescription)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PackageTypeDescription))
            .WithMessage("توضیحات بسته بندی بار نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}