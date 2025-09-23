using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.MoveDown;

public class MoveDownCompanyPackageTypeCommandValidator : AbstractValidator<MoveDownCompanyPackageTypeCommand>
{
    public MoveDownCompanyPackageTypeCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگتر از صفر باشد");
    }
}