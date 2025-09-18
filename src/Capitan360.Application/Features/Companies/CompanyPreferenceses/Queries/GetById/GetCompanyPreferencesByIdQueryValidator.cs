using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPreferenceses.Queries.GetCompanyPreferencesById;

public class GetCompanyPreferencesByIdQueryValidator : AbstractValidator<GetCompanyPreferencesByIdQuery>
{
    public GetCompanyPreferencesByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه تنظیمات باید بزرگتر از صفر باشد");
    }
}