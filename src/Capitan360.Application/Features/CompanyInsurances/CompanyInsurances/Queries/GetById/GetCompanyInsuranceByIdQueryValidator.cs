using FluentValidation;

namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Queries.GetById;

public class GetCompanyInsuranceByIdQueryValidator : AbstractValidator<GetCompanyInsuranceByIdQuery>
{
    public GetCompanyInsuranceByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه شرکت بیمه باید بزرگتر از صفر باشد");
    }
}
