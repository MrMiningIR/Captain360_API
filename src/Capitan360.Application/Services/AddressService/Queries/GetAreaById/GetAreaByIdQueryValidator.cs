using FluentValidation;

namespace Capitan360.Application.Services.AddressService.Queries.GetAreaById;

public class GetAreaByIdQueryValidator : AbstractValidator<GetAreaByIdQuery>
{
    public GetAreaByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه منطقه باید بزرگ‌تر از صفر باشد");
    }
}