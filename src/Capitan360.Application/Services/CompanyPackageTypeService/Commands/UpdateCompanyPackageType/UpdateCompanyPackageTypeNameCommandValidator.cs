using FluentValidation;

namespace Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateCompanyPackageType;

public class UpdateCompanyPackageTypeNameCommandValidator : AbstractValidator<UpdateCompanyPackageTypeNameCommand>
{
    public UpdateCompanyPackageTypeNameCommandValidator()
    {

        RuleFor(x => x.OriginalPackageId)
            .GreaterThan(0).WithMessage("شناسه نوع پکیج الزامی است");

        RuleFor(x => x.PackageTypeName)
            .MaximumLength(30).WithMessage("نام پکیج نمی‌تواند بیشتر از 30 کاراکتر باشد")
            .MinimumLength(3).WithMessage("نام پکیج نمی‌تواند کمتر از 3 کاراکتر باشد");

    }
}