using FluentValidation;

namespace Capitan360.Application.Services.Identity.Commands.AddUserToRole;

public class AddUserToRoleCommandValidator : AbstractValidator<AddUserToRoleCommand>
{
    public AddUserToRoleCommandValidator()
    {

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("شناسه کاربری نمیتواند خالی باشد");
        RuleFor(x => x.RoleId)
            .NotEmpty()
            .WithMessage("نقش نمیتواند خالی باشد");



    }
}