using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFromDistributionState;

public class BackFromDistributionStateCommandValidator : AbstractValidator<BackFromDistributionStateCommand>
{
    public BackFromDistributionStateCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بارنامه بزرگتر از صفر باشد");
    }
}
