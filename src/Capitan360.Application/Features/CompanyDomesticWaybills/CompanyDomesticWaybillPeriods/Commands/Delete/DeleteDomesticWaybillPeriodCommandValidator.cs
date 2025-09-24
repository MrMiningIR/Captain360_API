using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Commands.Delete;

public class DeleteDomesticWaybillPeriodCommandValidator : AbstractValidator<DeleteDomesticWaybillPeriodCommand>
{
    public DeleteDomesticWaybillPeriodCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه دوره باید بزرگتر از صفر باشد");
    }
}