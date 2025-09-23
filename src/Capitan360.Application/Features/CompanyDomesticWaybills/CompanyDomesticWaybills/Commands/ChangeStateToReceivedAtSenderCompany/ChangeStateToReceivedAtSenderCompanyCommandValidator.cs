using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToReceivedAtSenderCompany;

public class ChangeStateToReceivedAtSenderCompanyCommandValidator : AbstractValidator<ChangeStateToReceivedAtSenderCompanyCommand>
{
    public ChangeStateToReceivedAtSenderCompanyCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بارنامه بزرگتر از صفر باشد");
    }
}
