using FluentValidation;

namespace Capitan360.Application.Services.PackageTypeService.Commands.DeletePackageType;

public class DeletePackageTypeCommandValidator : AbstractValidator<DeletePackageTypeCommand>
{
    public DeletePackageTypeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه نوع محتوا باید بزرگ‌تر از صفر باشد");
    }
}