using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToDistribution;

public class ChangeStateToDistributionCommandValidator : AbstractValidator<ChangeStateToDistributionCommand>
{
    public ChangeStateToDistributionCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بارنامه بزرگتر از صفر باشد");
    }
}
