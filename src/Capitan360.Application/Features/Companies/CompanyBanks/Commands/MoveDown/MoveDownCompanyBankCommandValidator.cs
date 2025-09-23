using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyBanks.Commands.MoveDown;

public class MoveDownCompanyBankCommandValidator : AbstractValidator<MoveDownCompanyBankCommand>
{
    public MoveDownCompanyBankCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه بانک الزامی است");
    }
}
