using FluentValidation;

namespace Capitan360.Application.Features.Addresses.Addresses.Queries.GetById;

public class GetAddressByIdQueryValidator : AbstractValidator<GetAddressByIdQuery>
{
    public GetAddressByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه آدرس باید بزرگتر از صفر باشد");
    }
}