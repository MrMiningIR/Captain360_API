using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanySmsPatternses.Queries.GetByCompanyId;

public class GetCompanySmsPatternsByCompanyIdQueryValidator : AbstractValidator<GetCompanySmsPatternsByCompanyIdQuery>
{
    public GetCompanySmsPatternsByCompanyIdQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");
    }
}