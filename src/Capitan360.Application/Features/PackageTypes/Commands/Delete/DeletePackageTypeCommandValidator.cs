using FluentValidation;

namespace Capitan360.Application.Features.PackageTypes.Commands.Delete;

public class DeletePackageTypeCommandValidator : AbstractValidator<DeletePackageTypeCommand>
{
    public DeletePackageTypeCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگتر از صفر باشد");
    }
}