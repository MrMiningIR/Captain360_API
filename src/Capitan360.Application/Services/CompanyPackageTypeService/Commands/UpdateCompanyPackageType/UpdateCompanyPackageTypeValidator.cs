using FluentValidation;

namespace Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateCompanyPackageType;

public class UpdateCompanyPackageTypeValidator : AbstractValidator<UpdateCompanyPackageTypeCommand>
{
    public UpdateCompanyPackageTypeValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه نوع پکیج الزامی است");
        RuleFor(x => x.PackageTypeId)
            .GreaterThan(0).WithMessage("شناسه نوع پکیج الزامی است");
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت الزامی است");
        RuleFor(x => x.PackageTypeName)
            .MaximumLength(50).WithMessage("نام پکیج نمی‌تواند بیشتر از 50 کاراکتر باشد")
            .MinimumLength(3).WithMessage("نام پکیج نمی‌تواند کمتر از 3 کاراکتر باشد")
            .When(x => !string.IsNullOrEmpty(x.PackageTypeName));
    }
}