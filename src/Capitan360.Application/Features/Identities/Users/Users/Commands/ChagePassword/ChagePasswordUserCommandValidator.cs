using FluentValidation;

namespace Capitan360.Application.Features.Identities.Users.Users.Commands.ChagePassword;

public class ChagePasswordUserCommandValidator : AbstractValidator<ChagePasswordUserCommand>
{
    public ChagePasswordUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("شناسه کاربر الزامی است")
            .MaximumLength(450).WithMessage("شناسه کاربر نمی‌تواند بیشتر از 450 کاراکتر باشد");

        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("رمز عبور فعلی الزامی است")
            .MinimumLength(10).WithMessage("حداقل طول رمز عبور فعلی 10 کارکتر است")
            .Custom((pwd, ctx) =>
            {
                if (!pwd.Any(char.IsUpper)) ctx.AddFailure("رمز عبور فعلی باید حداقل یک حرف بزرگ انگلیسی داشته باشد.");
                if (!pwd.Any(char.IsLower)) ctx.AddFailure("رمز عبور فعلی باید حداقل یک حرف کوچک انگلیسی داشته باشد.");
                if (!pwd.Any(char.IsDigit)) ctx.AddFailure("رمز عبور فعلی باید حداقل یک عدد داشته باشد.");
                if (!pwd.Any(ch => !char.IsLetterOrDigit(ch)))
                    ctx.AddFailure("رمز عبور فعلی باید حداقل یک حرف خاص (مانند !@#$...) داشته باشد.");
            });

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("رمز عبور جدید الزامی است")
            .MinimumLength(10).WithMessage("حداقل طول رمز عبور جدید 10 کارکتر است")
            .Custom((pwd, ctx) =>
            {
                if (!pwd.Any(char.IsUpper)) ctx.AddFailure("رمز عبور جدید باید حداقل یک حرف بزرگ انگلیسی داشته باشد.");
                if (!pwd.Any(char.IsLower)) ctx.AddFailure("رمز عبور جدید باید حداقل یک حرف کوچک انگلیسی داشته باشد.");
                if (!pwd.Any(char.IsDigit)) ctx.AddFailure("رمز عبور باید حداقل یک عدد داشته باشد.");
                if (!pwd.Any(ch => !char.IsLetterOrDigit(ch)))
                    ctx.AddFailure("رمز عبور جدید باید حداقل یک حرف خاص (مانند !@#$...) داشته باشد.");
            });

        RuleFor(x => x.ConfirmNewPassword)
            .NotEmpty().WithMessage("تایید رمز عبور الزامی است")
            .Equal(x => x.NewPassword).WithMessage("تایید رمز عبور یکسان نیست.");
    }
}
