using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Commands.UpdateActiveState;

public class UpdateActiveStateCompanyDomesticWaybillPeriodCommandValidator : AbstractValidator<UpdateActiveStateCompanyDomesticWaybillPeriodCommand>
{
    public UpdateActiveStateCompanyDomesticWaybillPeriodCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه دوره باید بزرگتر از صفر باشد");
    }
}