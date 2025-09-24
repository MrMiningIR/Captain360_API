using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Queries.GetByCompanyId;

public class GetCompanyPackageTypeByCompanyIdQueryValidator : AbstractValidator<GetCompanyPackageTypeByCompanyIdQuery>
{
    public GetCompanyPackageTypeByCompanyIdQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");
    }
}
