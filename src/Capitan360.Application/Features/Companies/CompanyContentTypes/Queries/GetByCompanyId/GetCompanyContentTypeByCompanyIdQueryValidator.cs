using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Queries.GetByCompanyId;

public class GetCompanyContentTypeByCompanyIdQueryValidator : AbstractValidator<GetCompanyContentTypeByCompanyIdQuery>
{
    public GetCompanyContentTypeByCompanyIdQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");
    }
}
