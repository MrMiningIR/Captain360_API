using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateLock;

public class ChangeStateLockCompanyDomesticWaybillCommandValidator : AbstractValidator<ChangeStateLockCompanyDomesticWaybillCommand>
{
    public ChangeStateLockCompanyDomesticWaybillCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بارنامه بزرگتر از صفر باشد");
    }
}

