using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyUris.Queries.GetByCompanyId;

public class GetCompanyUriByCompanyIdQueryValidator : AbstractValidator<GetCompanyUriByCompanyIdQuery>
{
    public GetCompanyUriByCompanyIdQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");
    }
}