using FluentValidation;

namespace Capitan360.Application.Features.Identities.Identities.Commands.RemoveUserFromRole;

public class RemoveUserFromRoleCommandValidator : AbstractValidator<RemoveUserFromRoleCommand>
{
    public RemoveUserFromRoleCommandValidator()
    {

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("شناسه کاربری نمیتواند خالی باشد");
        RuleFor(x => x.RoleId)
            .NotEmpty()
            .WithMessage("نقش نمیتواند خالی باشد");



    }
}