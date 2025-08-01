using FluentValidation;

namespace Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateActiveStateCompanyPackageType;

public class UpdateActiveStatePackageTypeValidator : AbstractValidator<UpdateActiveStateCompanyPackageTypeCommand>
{
    public UpdateActiveStatePackageTypeValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه بسته بندی باید بزرگ‌تر از صفر باشد");
    }
}