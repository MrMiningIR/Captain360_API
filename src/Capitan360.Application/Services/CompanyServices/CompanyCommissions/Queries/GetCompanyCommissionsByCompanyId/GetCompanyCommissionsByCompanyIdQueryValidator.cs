using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyCommissions.Queries.GetCompanyCommissionsByCompanyId;

public class GetCompanyCommissionsByCompanyIdQueryValidator : AbstractValidator<GetCompanyCommissionsByCompanyId.GetCompanyCommissionsByCompanyIdQuery>
{
    public GetCompanyCommissionsByCompanyIdQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");
    }
}