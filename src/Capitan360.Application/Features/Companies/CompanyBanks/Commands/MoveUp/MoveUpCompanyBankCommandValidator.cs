using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyBanks.Commands.MoveUp;

public class MoveUpCompanyBankCommandValidator : AbstractValidator<MoveUpCompanyBankCommand>
{
    public MoveUpCompanyBankCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه بانک الزامی است");
    }
}
