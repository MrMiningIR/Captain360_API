using FluentValidation;

namespace Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateCompanyPackageType;

public class UpdateCompanyPackageTypeCommandValidator : AbstractValidator<UpdateCompanyPackageTypeCommand>
{
    public UpdateCompanyPackageTypeCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه نوع بسته بندی شرکت الزامی است");
        RuleFor(x => x.PackageTypeId)
            .GreaterThan(0).WithMessage("شناسه نوع بسته بندی الزامی است");
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت الزامی است");
        RuleFor(x => x.CompanyPackageTypeName)
            .MaximumLength(50).WithMessage("نام پکیج نمی‌تواند بیشتر از 50 کاراکتر باشد")
            .MinimumLength(3).WithMessage("نام پکیج نمی‌تواند کمتر از 3 کاراکتر باشد")
            .When(x => !string.IsNullOrEmpty(x.CompanyPackageTypeName));
    }
}