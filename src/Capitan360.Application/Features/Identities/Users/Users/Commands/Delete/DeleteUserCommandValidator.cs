using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capitan360.Application.Features.Identities.Roles.Roles.Commands.Delete;
using FluentValidation;

namespace Capitan360.Application.Features.Identities.Users.Users.Commands.Delete;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("شناسه کاربر الزامی است")
            .MaximumLength(450).WithMessage("شناسه کاربر نمی‌تواند بیشتر از 450 کاراکتر باشد");
    }
}
