using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToCollection;

public class ChangeStateToCollectionCommandValidator : AbstractValidator<ChangeStateToCollectionCommand>
{
    public ChangeStateToCollectionCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بارنامه بزرگتر از صفر باشد");
    }
}
