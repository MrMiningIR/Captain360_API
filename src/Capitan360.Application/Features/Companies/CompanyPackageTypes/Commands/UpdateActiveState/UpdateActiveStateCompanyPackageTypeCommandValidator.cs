using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateActiveState;

public class UpdateActiveStatePackageTypeValidator : AbstractValidator<UpdateActiveStateCompanyPackageTypeCommand>
{
    public UpdateActiveStatePackageTypeValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگتر از صفر باشد");
    }
}