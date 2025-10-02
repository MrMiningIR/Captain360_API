using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Queries.GetById;

public class GetCompanyDomesticPathReceiverCompanyByIdQueryValidator : AbstractValidator<GetCompanyDomesticPathReceiverCompanyByIdQuery>
{
    public GetCompanyDomesticPathReceiverCompanyByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه مسیر باید بزرگتر از صفر باشد");
    }
}