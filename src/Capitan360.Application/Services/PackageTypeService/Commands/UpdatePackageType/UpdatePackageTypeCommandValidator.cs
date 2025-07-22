using FluentValidation;

namespace Capitan360.Application.Services.PackageTypeService.Commands.UpdatePackageType;

public class UpdatePackageTypeCommandValidator : AbstractValidator<UpdatePackageTypeCommand>
{
    public UpdatePackageTypeCommandValidator()
    {
        RuleFor(x => x.CompanyTypeId)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.PackageTypeName)
            .NotEmpty().WithMessage("نام نوع محتوا نمی‌تواند خالی باشد")
            .MaximumLength(50).WithMessage("نام نوع محتوا نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.PackageTypeDescription)
            .NotEmpty().WithMessage("توضیحات نوع محتوا نمی‌تواند خالی باشد")
            .MaximumLength(50).WithMessage("توضیحات نوع محتوا نمی‌تواند بیشتر از 50 کاراکتر باشد");


    }
}