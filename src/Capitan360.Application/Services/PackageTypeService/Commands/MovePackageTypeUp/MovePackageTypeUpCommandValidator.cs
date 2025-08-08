using FluentValidation;

namespace Capitan360.Application.Services.PackageTypeService.Commands.MovePackageTypeUp;

public class MovePackageTypeUpCommandValidator : AbstractValidator<MovePackageTypeUpCommand>
{
    public MovePackageTypeUpCommandValidator()
    {

        RuleFor(x => x.PackageTypeId)
           .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگ‌تر از صفر باشد");
    }
}