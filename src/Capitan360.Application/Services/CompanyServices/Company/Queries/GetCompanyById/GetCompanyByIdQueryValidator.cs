using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.Company.Queries.GetCompanyById;

public class GetCompanyByIdQueryValidator : AbstractValidator<GetCompanyByIdQuery>
{
    public GetCompanyByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");
    }
}