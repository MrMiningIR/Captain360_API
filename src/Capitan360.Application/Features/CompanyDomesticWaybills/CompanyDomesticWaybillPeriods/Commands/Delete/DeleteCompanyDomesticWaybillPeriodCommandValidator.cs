using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Commands.Delete;

public class DeleteCompanyDomesticWaybillPeriodCommandValidator : AbstractValidator<DeleteCompanyDomesticWaybillPeriodCommand>
{
    public DeleteCompanyDomesticWaybillPeriodCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه دوره باید بزرگتر از صفر باشد");
    }
}