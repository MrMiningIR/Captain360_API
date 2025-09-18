using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Queries.GetById;

public class GetCompanyPackageTypeByIdQueryValidator : AbstractValidator<GetCompanyPackageTypeByIdQuery>
{
    public GetCompanyPackageTypeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگتر از صفر باشد");
    }
}