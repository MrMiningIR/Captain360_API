using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToBackToSenderCompany;

public class ChangeStateDomesticWaybillToBackToSenderCompanyCommandValidaton : AbstractValidator<ChangeStateDomesticWaybillToBackToSenderCompanyCommand>
{
    public ChangeStateDomesticWaybillToBackToSenderCompanyCommandValidaton()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بارنامه بزرگتر از صفر باشد");
    }
}
