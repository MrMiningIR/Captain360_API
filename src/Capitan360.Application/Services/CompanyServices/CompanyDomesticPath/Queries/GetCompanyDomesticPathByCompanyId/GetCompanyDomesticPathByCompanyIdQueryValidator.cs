using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Queries.GetCompanyDomesticPathByCompanyId;

public class GetCompanyDomesticPathByCompanyIdQueryValidator : AbstractValidator<GetCompanyDomesticPathByCompanyIdQuery>
{
    public GetCompanyDomesticPathByCompanyIdQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");
    }
}