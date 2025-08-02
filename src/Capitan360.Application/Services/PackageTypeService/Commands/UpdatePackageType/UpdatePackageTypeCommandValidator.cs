using FluentValidation;

namespace Capitan360.Application.Services.PackageTypeService.Commands.UpdatePackageType;

public class UpdatePackageTypeCommandValidator : AbstractValidator<UpdatePackageTypeCommand>
{
    public UpdatePackageTypeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه نوع بسته بندی باید بزرگ‌تر از صفر باشد");
        RuleFor(x => x.CompanyTypeId)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.PackageTypeName)
            .NotEmpty().WithMessage("نام نوع محتوی نمی‌تواند خالی باشد")
            .MaximumLength(50).WithMessage("نام نوع محتوی نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.PackageTypeDescription)
            .MaximumLength(500).WithMessage("توضیحات نوع محتوی نمی‌تواند بیشتر از 500 کاراکتر باشد")
     .When(x => !string.IsNullOrEmpty(x.PackageTypeDescription));
    }
}