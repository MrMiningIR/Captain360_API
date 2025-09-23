using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPreferenceses.Queries.GetByCompanyId;

public class GetCompanyPreferencesByCompanyIdQueryValidator : AbstractValidator<GetCompanyPreferencesByCompanyIdQuery>
{
    public GetCompanyPreferencesByCompanyIdQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");
    }
}