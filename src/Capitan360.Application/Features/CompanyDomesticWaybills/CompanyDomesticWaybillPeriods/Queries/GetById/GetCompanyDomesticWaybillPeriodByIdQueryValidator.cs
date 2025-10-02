using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Queries.GetById;

public class GetCompanyDomesticWaybillPeriodByIdQueryValidator : AbstractValidator<GetCompanyDomesticWaybillPeriodByIdQuery>
{
    public GetCompanyDomesticWaybillPeriodByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه مخزن باید بزرگتر از صفر باشد");
    }
}
