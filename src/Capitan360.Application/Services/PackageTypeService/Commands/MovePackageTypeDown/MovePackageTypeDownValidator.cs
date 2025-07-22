using FluentValidation;

namespace Capitan360.Application.Services.PackageTypeService.Commands.MovePackageTypeDown;

public class MovePackageTypeDownValidator : AbstractValidator<MovePackageTypeDownCommand>
{


    public MovePackageTypeDownValidator()
    {

        RuleFor(x => x.CompanyTypeId)
            .GreaterThan(0).WithMessage("شناسه CompanyType باید بزرگ‌تر از صفر باشد");
        RuleFor(x => x.PackageTypeId)
            .GreaterThan(0).WithMessage("شناسه ContentType باید بزرگ‌تر از صفر باشد");
    }
}