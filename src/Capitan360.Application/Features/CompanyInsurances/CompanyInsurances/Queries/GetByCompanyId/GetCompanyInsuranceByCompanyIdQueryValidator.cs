using FluentValidation;

namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Queries.GetByCompanyId;

public class GetCompanyInsuranceByCompanyIdQueryValidator : AbstractValidator<GetCompanyInsuranceByCompanyIdQuery>
{
    public GetCompanyInsuranceByCompanyIdQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");
    }
}
