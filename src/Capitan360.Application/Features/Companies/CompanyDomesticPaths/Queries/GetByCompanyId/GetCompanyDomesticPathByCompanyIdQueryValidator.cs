using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyDomesticPaths.Queries.GetByCompanyId;

public class GetCompanyDomesticPathByCompanyIdQueryValidator : AbstractValidator<GetCompanyDomesticPathByCompanyIdQuery>
{
    public GetCompanyDomesticPathByCompanyIdQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");
    }
}