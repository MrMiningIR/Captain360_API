using FluentValidation;

namespace Capitan360.Application.Features.PackageTypes.Commands.MoveDown;

public class MoveDownPackageTypeCommandValidator : AbstractValidator<MoveDownPackageTypeCommand>
{
    public MoveDownPackageTypeCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگتر از صفر باشد");
    }
}