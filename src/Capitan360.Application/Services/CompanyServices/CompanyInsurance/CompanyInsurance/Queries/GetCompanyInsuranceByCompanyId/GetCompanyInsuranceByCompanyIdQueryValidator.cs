using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Queries.GetCompanyInsuranceByCompanyId;

public class GetCompanyInsuranceByCompanyIdQueryValidator : AbstractValidator<GetCompanyInsuranceByCompanyIdQuery>
{
    public GetCompanyInsuranceByCompanyIdQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");
    }
}