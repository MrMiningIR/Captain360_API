using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Queries.GetCompanySmsPatternsById;

public class GetCompanySmsPatternsByIdQueryValidator : AbstractValidator<GetCompanySmsPatternsByIdQuery>
{
    public GetCompanySmsPatternsByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه الگوهای SMS باید بزرگ‌تر از صفر باشد");
    }
}