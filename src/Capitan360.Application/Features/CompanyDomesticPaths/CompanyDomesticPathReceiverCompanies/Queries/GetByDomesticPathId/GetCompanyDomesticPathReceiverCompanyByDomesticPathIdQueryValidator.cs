using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Queries.GetByDomesticPathId;

public class GetCompanyDomesticPathReceiverCompanyByDomesticPathIdQueryValidator : AbstractValidator<GetCompanyDomesticPathReceiverCompanyByDomesticPathIdQuery>
{
    public GetCompanyDomesticPathReceiverCompanyByDomesticPathIdQueryValidator()
    {
        RuleFor(x => x.DomesticPathId)
            .GreaterThan(0).WithMessage("شناسه مسیر شرکت باید بزرگتر از صفر باشد");
    }
}
