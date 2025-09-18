using FluentValidation;

namespace Capitan360.Application.Features.PackageTypeService.Commands.MoveTypeDown;

public class MovePackageTypeDownCommandValidator : AbstractValidator<MovePackageTypeDownCommand>
{
    public MovePackageTypeDownCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگتر از صفر باشد");
    }
}