using Capitan360.Domain.Enums;
using FluentValidation;

namespace Capitan360.Application.Services.Identity.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{


    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("FullName is required")
            .MaximumLength(100)
            .WithMessage("FullName must not exceed 100 characters")
            .MinimumLength(5)
            .WithMessage("FullName must be at least 5 characters");

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

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("Passwords do not match");

        RuleFor(dto => dto.MoadianFactorType)
            .Must(moadianType => Enum.IsDefined(typeof(MoadianFactorType), moadianType))
            .When(x => x.MoadianFactorType != 0)
            .WithMessage($"Invalid MoadianFactorType. Valid values are: {string.Join(", ", Enum.GetValues(typeof(MoadianFactorType)))}");


        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("ایمیل باید به فرمت معتبر باشد");

        //RuleFor(x => x.CompanyId)
        //    .GreaterThan(0)
        //    .When(x => x.CompanyId.HasValue); 

    }
}



