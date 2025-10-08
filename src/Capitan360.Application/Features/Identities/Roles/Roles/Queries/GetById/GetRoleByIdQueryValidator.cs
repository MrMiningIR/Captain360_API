using FluentValidation;

namespace Capitan360.Application.Features.Identities.Roles.Roles.Queries.GetById;

public class GetRoleByIdQueryValidator : AbstractValidator<GetRoleByIdQuery>
{
    public GetRoleByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("شناسه نقش الزامی است.")
            .MaximumLength(450).WithMessage("حداکثر طول شناسه نقش 450 کاراکتر است.");
    }
}
