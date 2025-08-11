using FluentValidation;

namespace Capitan360.Application.Services.PackageTypeService.Commands.CreatePackageType;

public class CreatePackageTypeValidator : AbstractValidator<CreatePackageTypeCommand>
{
    public CreatePackageTypeValidator()
    {
        RuleFor(x => x.CompanyTypeId)
     .GreaterThan(0).WithMessage("شناسه نوع شرکت باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.PackageTypeName)
            .NotEmpty().WithMessage("نام بسته بندی الزامی است")
            .MinimumLength(4).WithMessage("نام بسته بندی نمی‌تواند کمتر از 4 کاراکتر باشد")
            .MaximumLength(50).WithMessage("نام بسته بندی نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.PackageTypeDescription)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.PackageTypeDescription))
            .WithMessage("توضیحات بسته بندی نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}