using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToReceivedAtReceiverCompany;

public class ChangeStateCompanyDomesticWaybillToReceivedAtReceiverCompanyCommandValidator : AbstractValidator<ChangeStateCompanyDomesticWaybillToReceivedAtReceiverCompanyCommand>
{
    public ChangeStateCompanyDomesticWaybillToReceivedAtReceiverCompanyCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه فرم مانیسفست باید بزرگتر از صفر باشد");
    }
}
