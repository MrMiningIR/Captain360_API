using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFormReceivedAtSenderCompanyState;

public class BackFormReceivedAtSenderCompanyStateCommandValidator : AbstractValidator<BackFormReceivedAtSenderCompanyStateCommand>
{
    public BackFormReceivedAtSenderCompanyStateCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بارنامه بزرگتر از صفر باشد");
    }
}
