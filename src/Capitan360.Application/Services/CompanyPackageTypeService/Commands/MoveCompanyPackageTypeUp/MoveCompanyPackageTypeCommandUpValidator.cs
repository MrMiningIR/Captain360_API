using FluentValidation;

namespace Capitan360.Application.Services.CompanyPackageTypeService.Commands.MoveCompanyPackageTypeUp;

public class MoveCompanyPackageTypeCommandUpValidator : AbstractValidator<MoveCompanyPackageTypeUpCommand>
{
    public MoveCompanyPackageTypeCommandUpValidator()
    {

        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگتر از صفر باشد");
    }
}