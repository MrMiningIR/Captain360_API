using FluentValidation;

namespace Capitan360.Application.Services.Identity.Commands.ChangeUserActivity;

public class ChangeUserActivityCommandValidator : AbstractValidator<ChangeUserActivityCommand>
{
    public ChangeUserActivityCommandValidator()
    {

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("شناسه کاربری نمیتواند خالی باشد");



    }
}