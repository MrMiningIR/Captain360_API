using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFromReceivedAtReceiverCompanyState;

public class BackCompanyDomesticWaybillFromReceivedAtReceiverCompanyStateCommandValidator : AbstractValidator<BackCompanyDomesticWaybillFromReceivedAtReceiverCompanyStateCommand>
{
    public BackCompanyDomesticWaybillFromReceivedAtReceiverCompanyStateCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه فرم مانیسفست باید بزرگتر از صفر باشد");
    }
}
