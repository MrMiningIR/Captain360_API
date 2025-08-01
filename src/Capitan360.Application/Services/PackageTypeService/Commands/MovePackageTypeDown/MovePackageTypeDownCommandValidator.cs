using FluentValidation;

namespace Capitan360.Application.Services.PackageTypeService.Commands.MovePackageTypeDown;

public class MovePackageTypeDownCommandValidator : AbstractValidator<MovePackageTypeDownCommand>
{
    public MovePackageTypeDownCommandValidator()
    {
        RuleFor(x => x.CompanyTypeId)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت باید بزرگ‌تر از صفر باشد");
        RuleFor(x => x.PackageTypeId)
            .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگ‌تر از صفر باشد");
    }
}