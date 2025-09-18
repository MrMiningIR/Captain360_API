using FluentValidation;

namespace Capitan360.Application.Features.PackageTypeService.Commands.MoveUp;

public class MovePackageTypeUpCommandValidator : AbstractValidator<MovePackageTypeUpCommand>
{
    public MovePackageTypeUpCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگتر از صفر باشد");
    }
}