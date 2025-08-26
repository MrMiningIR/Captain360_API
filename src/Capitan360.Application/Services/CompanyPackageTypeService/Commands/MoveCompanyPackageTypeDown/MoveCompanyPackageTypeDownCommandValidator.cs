using FluentValidation;

namespace Capitan360.Application.Services.CompanyPackageTypeService.Commands.MoveCompanyPackageTypeDown;

public class MoveCompanyPackageTypeDownCommandValidator : AbstractValidator<MoveCompanyPackageTypeDownCommand>
{
    public MoveCompanyPackageTypeDownCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگتر از صفر باشد");
    }
}