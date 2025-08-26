using FluentValidation;

namespace Capitan360.Application.Services.PackageTypeService.Commands.UpdatePackageType;

public class UpdatePackageTypeCommandValidator : AbstractValidator<UpdatePackageTypeCommand>
{
    public UpdatePackageTypeCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگتر از صفر باشد");

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