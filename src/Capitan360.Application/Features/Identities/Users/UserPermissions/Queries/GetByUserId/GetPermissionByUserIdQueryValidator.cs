using FluentValidation;

namespace Capitan360.Application.Features.Identities.Users.UserPermissions.Queries.GetByUserId;

public class GetPermissionByUserIdQueryValidator : AbstractValidator<GetPermissionByUserIdQuery>
{
    public GetPermissionByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("شناسه کاربر الزامی است.")
            .MaximumLength(450).WithMessage("حداکثر طول شناسه کاربر 450 کاراکتر است.");
    }
}
