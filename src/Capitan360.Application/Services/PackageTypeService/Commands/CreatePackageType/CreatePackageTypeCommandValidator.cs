using FluentValidation;

namespace Capitan360.Application.Services.PackageTypeService.Commands.CreatePackageType;

public class CreatePackageTypeValidator : AbstractValidator<CreatePackageTypeCommand>
{
    public CreatePackageTypeValidator()
    {
        RuleFor(x => x.CompanyTypeId)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت الزامی است");

        RuleFor(x => x.PackageTypeName)
            .NotEmpty().WithMessage("نام نوع محتوی الزامی است")
            .MaximumLength(50).WithMessage("نام نوع محتوی نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.PackageTypeDescription)
            .NotEmpty().WithMessage("توضیحات نوع محتوی الزامی است")
            .MaximumLength(50).WithMessage("توضیحات نوع محتوی نمی‌تواند بیشتر از 50 کاراکتر باشد");
    }
}