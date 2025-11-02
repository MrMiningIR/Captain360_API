using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFormBackToSenderCompanyState;

public class BackDomesticWaybillFormBackToSenderCompanyStateCommandValidaton : AbstractValidator<BackDomesticWaybillFormBackToSenderCompanyStateCommand>
{
    public BackDomesticWaybillFormBackToSenderCompanyStateCommandValidaton()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بارنامه بزرگتر از صفر باشد");
    }
}

