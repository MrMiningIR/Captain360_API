using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPreferenceses.Queries.GetCompanyPreferencesByCompanyId;

internal class GetCompanyPreferencesByCompanyIdQueryValidator : AbstractValidator<GetCompanyPreferencesByCompanyIdQuery>
{
    public GetCompanyPreferencesByCompanyIdQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");
    }
}