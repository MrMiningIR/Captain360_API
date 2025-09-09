using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Queries.GetCompanySmsPatternsByCompanyId;

internal class GetCompanySmsPatternsByCompanyIdQueryValidator : AbstractValidator<GetCompanySmsPatternsByCompanyId.GetCompanySmsPatternsByCompanyIdQuery>
{
    public GetCompanySmsPatternsByCompanyIdQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");
    }
}