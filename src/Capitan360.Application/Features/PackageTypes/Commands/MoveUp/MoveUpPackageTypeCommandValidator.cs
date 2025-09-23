using FluentValidation;

namespace Capitan360.Application.Features.PackageTypes.Commands.MoveUp;

public class MoveUpPackageTypeCommandValidator : AbstractValidator<MoveUpPackageTypeCommand>
{
    public MoveUpPackageTypeCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگتر از صفر باشد");
    }
}