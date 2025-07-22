using FluentValidation;

namespace Capitan360.Application.Services.CompanyPackageTypeService.Commands.MoveCompanyPackageTypeUp;

public class MoveCompanyPackageTypeUpValidator : AbstractValidator<MoveCompanyPackageTypeUpCommand>
{
    public MoveCompanyPackageTypeUpValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه Company باید بزرگ‌تر از صفر باشد");
        RuleFor(x => x.PackageTypeId)
            .GreaterThan(0).WithMessage("شناسه PackageType باید بزرگ‌تر از صفر باشد");
    }
}