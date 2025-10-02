using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Queries.GetByCompanyDomesticWaybillId;

public class GetDomesticWaybillPackageTypeByDomesticWaybillIdQueryValidator : AbstractValidator<GetDomesticWaybillPackageTypeByDomesticWaybillIdQuery>
{
    public GetDomesticWaybillPackageTypeByDomesticWaybillIdQueryValidator()
    {
        RuleFor(x => x.CompanyDomesticWaybillId)
            .GreaterThan(0).WithMessage("شناسه بارنامه باید بزرگتر از صفر باشد");
    }
}
