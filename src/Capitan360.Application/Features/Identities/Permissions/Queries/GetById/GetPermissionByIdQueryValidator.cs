using FluentValidation;

namespace Capitan360.Application.Features.Identities.Permissions.Queries.GetById;

public class GetPermissionByIdQueryValidator : AbstractValidator<GetPermissionByIdQuery>
{
    public GetPermissionByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه مجوز باید بزرگتر از صفر باشد");
    }
}

