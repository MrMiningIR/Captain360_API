using FluentValidation;

namespace Capitan360.Application.Services.Identity.Queries.LoginUser;

public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
{
    public LoginUserQueryValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("PhoneNumber is required")
            .Matches(@"^\d{11}$")
            .WithMessage("PhoneNumber must be 11 digits");
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters")
            .Matches(@"[A-Za-z0-9]+")
            .WithMessage("Password must contain only letters or numbers");
    }
}