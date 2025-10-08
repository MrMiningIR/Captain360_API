using FluentValidation;

namespace Capitan360.Application.Features.Addresses.Addresses.Queries.GetByUserId;

public class GetAddressByUserIdQueryValidator : AbstractValidator<GetAddressByUserIdQuery>
{
    public GetAddressByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("شناسه کاربر الزامی است.")
            .MaximumLength(450).WithMessage("حداکثر طول شناسه کاربر 450 کاراکتر است.");
    }
}