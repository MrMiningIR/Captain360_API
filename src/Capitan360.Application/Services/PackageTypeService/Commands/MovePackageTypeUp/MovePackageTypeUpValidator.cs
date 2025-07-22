using FluentValidation;

namespace Capitan360.Application.Services.PackageTypeService.Commands.MovePackageTypeUp;

public class MovePackageTypeUpValidator : AbstractValidator<MovePackageTypeUpCommand>
{
    public MovePackageTypeUpValidator()
    {

        RuleFor(x => x.CompanyTypeId)
            .GreaterThan(0).WithMessage("شناسه CompanyType باید بزرگ‌تر از صفر باشد");
        RuleFor(x => x.PackageTypeId)
            .GreaterThan(0).WithMessage("شناسه ContentType باید بزرگ‌تر از صفر باشد");
    }
}