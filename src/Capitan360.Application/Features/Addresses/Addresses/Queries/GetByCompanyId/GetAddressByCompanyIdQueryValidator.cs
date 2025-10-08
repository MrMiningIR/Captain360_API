using FluentValidation;

namespace Capitan360.Application.Features.Addresses.Addresses.Queries.GetByCompanyId;

public class GetAddressByCompanyIdQueryValidator : AbstractValidator<GetAddressByCompanyIdQuery>
{
    public GetAddressByCompanyIdQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");
    }
}
