using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToAirlineDelivery;

public class ChangeStateCompanyDomesticWaybillToAirlineDeliveryCommandValidator : AbstractValidator<ChangeStateCompanyDomesticWaybillToAirlineDeliveryCommand>
{
    public ChangeStateCompanyDomesticWaybillToAirlineDeliveryCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بارنامه باید بزرگتر از صفر باشد");
    }
}
