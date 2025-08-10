using FluentValidation;

namespace Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateCompanyPackageTypeName;

public class UpdateCompanyPackageTypeNameAndDescriptionCommandValidator : AbstractValidator<UpdateCompanyPackageTypeNameAndDescriptionCommand>
{
    public UpdateCompanyPackageTypeNameAndDescriptionCommandValidator()
    {
        RuleFor(x => x.PackageTypeName)
            .MaximumLength(30).WithMessage("نام پکیج نمی‌تواند بیشتر از 30 کاراکتر باشد")
            .MinimumLength(3).WithMessage("نام پکیج نمی‌تواند کمتر از 3 کاراکتر باشد");
    }
}