using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFormCollectiongState;

public class BackCompanyDomesticWaybillFormCollectiongStateCommandValidator : AbstractValidator<BackCompanyDomesticWaybillFromCollectiongStateCommand>
{
    public BackCompanyDomesticWaybillFormCollectiongStateCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بارنامه بزرگتر از صفر باشد");
    }
}
