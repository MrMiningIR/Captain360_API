using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.MoveUp;

public class MoveCompanyPackageTypeCommandUpValidator : AbstractValidator<MoveCompanyPackageTypeUpCommand>
{
    public MoveCompanyPackageTypeCommandUpValidator()
    {

        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگتر از صفر باشد");
    }
}