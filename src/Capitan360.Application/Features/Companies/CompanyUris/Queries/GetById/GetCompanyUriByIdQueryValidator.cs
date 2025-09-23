using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyUris.Queries.GetById;

public class GetCompanyUriByIdQueryValidator : AbstractValidator<GetCompanyUriByIdQuery>
{
    public GetCompanyUriByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه URI باید بزرگتر از صفر باشد");
    }
}