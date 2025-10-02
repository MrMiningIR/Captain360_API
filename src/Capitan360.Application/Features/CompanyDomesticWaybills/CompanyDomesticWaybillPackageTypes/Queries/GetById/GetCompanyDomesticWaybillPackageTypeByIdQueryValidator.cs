using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Queries.GetById;

public class GetCompanyDomesticWaybillPackageTypeByIdQueryValidator : AbstractValidator<GetCompanyDomesticWaybillPackageTypeByIdQuery>
{
    public GetCompanyDomesticWaybillPackageTypeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه بسته مرسوله باید بزرگتر از صفر باشد");
    }
}