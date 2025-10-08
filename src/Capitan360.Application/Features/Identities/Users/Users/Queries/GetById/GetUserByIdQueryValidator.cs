using Capitan360.Application.Features.Addresses.Addresses.Queries.GetByUserId;
using FluentValidation;

namespace Capitan360.Application.Features.Identities.Users.Users.Queries.GetById;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id!)
            .NotEmpty().WithMessage("شناسه کاربر الزامی است.")
            .MaximumLength(450).WithMessage("حداکثر طول شناسه کاربر 450 کاراکتر است.");
    }
}
