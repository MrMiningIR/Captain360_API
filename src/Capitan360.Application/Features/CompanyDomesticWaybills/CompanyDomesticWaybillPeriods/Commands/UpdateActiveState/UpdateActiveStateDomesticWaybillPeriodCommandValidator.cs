using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Commands.UpdateActiveState;

public class UpdateActiveStateDomesticWaybillPeriodCommandValidator : AbstractValidator<UpdateActiveStateDomesticWaybillPeriodCommand>
{
    public UpdateActiveStateDomesticWaybillPeriodCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");
    }
}