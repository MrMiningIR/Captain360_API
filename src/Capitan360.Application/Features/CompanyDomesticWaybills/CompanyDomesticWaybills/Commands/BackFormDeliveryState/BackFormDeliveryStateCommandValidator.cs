using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFormDeliveryState;

public class BackFormDeliveryStateCommandValidator : AbstractValidator<BackFormDeliveryStateCommand>
{
    public BackFormDeliveryStateCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بارنامه بزرگتر از صفر باشد");
    }
}
