using FluentValidation;

namespace Capitan360.Application.Services.CompanyPackageTypeService.Commands.MoveCompanyPackageTypeUp;

public class MoveCompanyPackageTypeCommandUpValidator : AbstractValidator<MoveCompanyPackageTypeUpCommand>
{
    public MoveCompanyPackageTypeCommandUpValidator()
    {
        RuleFor(x => x.CompanyId)
     .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");
        RuleFor(x => x.PackageTypeId)
            .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگ‌تر از صفر باشد");
    }
}