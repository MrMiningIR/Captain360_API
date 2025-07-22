using FluentValidation;

namespace Capitan360.Application.Services.UserPermission.Queries.GetUserPermissions;

public class GetUserPermissionsQueryValidator : AbstractValidator<GetUserPermissionsQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30, 50, 100];

    public GetUserPermissionsQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("شناسه کاربری نمیتواند خالی باشد");

        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", _allowPageSizes)}]");
    }
}