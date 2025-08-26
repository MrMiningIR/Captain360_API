using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyUri.Queries.GetCompanyUriById;

public class GetCompanyUriByIdQueryValidator : AbstractValidator<GetCompanyUriByIdQuery>
{
    public GetCompanyUriByIdQueryValidator()
    {
        RuleFor(x => x.Id)
              .GreaterThan(0).WithMessage("شناسه URI باید بزرگتر از صفر باشد");
    }
}