using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.MoveUp;

public class MoveUpCompanyPackageTypeCommandValidator : AbstractValidator<MoveUpCompanyPackageTypeCommand>
{
    public MoveUpCompanyPackageTypeCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگتر از صفر باشد");
    }
}