using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyTypes.Queries.GetById;

public class GetCompanyTypeByIdQueryValidator : AbstractValidator<GetCompanyTypeByIdQuery>
{
    public GetCompanyTypeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت باید بزرگتر از صفر باشد");
    }
}