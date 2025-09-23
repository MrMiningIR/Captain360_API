using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyBanks.Commands.UpdateActiveState;

public class UpdateActiveStateCompanyBankCommandValidator : AbstractValidator<UpdateActiveStateCompanyBankCommand>
{
    public UpdateActiveStateCompanyBankCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه بانک الزامی است");
    }
}
