using FluentValidation;

namespace Capitan360.Application.Services.PackageTypeService.Commands.UpdateActiveStatePackageType;

public class UpdateActiveStatePackageTypeCommandValidator : AbstractValidator<UpdateActiveStatePackageTypeCommand>
{
    public UpdateActiveStatePackageTypeCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگتر از صفر باشد");
    }
}