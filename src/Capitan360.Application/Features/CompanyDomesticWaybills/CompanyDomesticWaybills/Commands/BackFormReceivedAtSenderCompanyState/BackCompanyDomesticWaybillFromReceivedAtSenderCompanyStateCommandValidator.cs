using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFormReceivedAtSenderCompanyState;

public class BackCompanyDomesticWaybillFromReceivedAtSenderCompanyStateCommandValidator : AbstractValidator<BackCompanyDomesticWaybillFromReceivedAtSenderCompanyStateCommand>
{
    public BackCompanyDomesticWaybillFromReceivedAtSenderCompanyStateCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بارنامه بزرگتر از صفر باشد");
    }
}
