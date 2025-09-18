using FluentValidation;

namespace Capitan360.Application.Features.Identities.Identities.Commands.ChangeUserActivity;

public class ChangeUserActivityCommandValidator : AbstractValidator<ChangeUserActivityCommand>
{
    public ChangeUserActivityCommandValidator()
    {

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("شناسه کاربری نمیتواند خالی باشد");



    }
}