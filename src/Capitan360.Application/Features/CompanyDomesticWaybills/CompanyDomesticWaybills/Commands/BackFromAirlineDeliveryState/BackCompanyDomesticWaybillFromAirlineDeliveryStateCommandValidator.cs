using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFromAirlineDeliveryState;

public class BackCompanyDomesticWaybillFromAirlineDeliveryStateCommandValidator : AbstractValidator<BackCompanyDomesticWaybillFromAirlineDeliveryStateCommand>
{
    public BackCompanyDomesticWaybillFromAirlineDeliveryStateCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بارنامه باید بزرگتر از صفر باشد");
    }
}
