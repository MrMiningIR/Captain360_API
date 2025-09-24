using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanySmsPatternses.Queries.GetById;

public class GetCompanySmsPatternsByIdQueryValidator : AbstractValidator<GetCompanySmsPatternsByIdQuery>
{
    public GetCompanySmsPatternsByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه الگوهای SMS باید بزرگتر از صفر باشد");
    }
}