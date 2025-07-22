using FluentValidation;

namespace Capitan360.Application.Services.CompanyPackageTypeService.Commands.MoveCompanyPackageTypeDown;

public class MoveCompanyPackageTypeDownValidator : AbstractValidator<MoveCompanyPackageTypeDownCommand>
{
    public MoveCompanyPackageTypeDownValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه Company باید بزرگ‌تر از صفر باشد");
        RuleFor(x => x.PackageTypeId)
            .GreaterThan(0).WithMessage("شناسه PackageType باید بزرگ‌تر از صفر باشد");
    }
}