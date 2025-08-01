using FluentValidation;

namespace Capitan360.Application.Services.PackageTypeService.Commands.UpdateActiveStatePackageType;

public class UpdateActiveStatePackageTypeValidator : AbstractValidator<UpdateActiveStatePackageTypeCommand>
{
    public UpdateActiveStatePackageTypeValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه بسته بندی باید بزرگ‌تر از صفر باشد");
    }
}