using Capitan360.Domain.Enums;
using FluentValidation;

namespace Capitan360.Application.Services.Identity.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{


    public UpdateUserCommandValidator()
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



        RuleFor(x => x.MoadianFactorType)
            .Must(moadianType => Enum.IsDefined(typeof(MoadianFactorType), moadianType))
            .WithMessage($"Invalid MoadianFactorType. Valid values are: {string.Join(", ", Enum.GetValues(typeof(MoadianFactorType)))}");


        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("ایمیل باید به فرمت معتبر باشد");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("شناسه کاربری نمیتواند خالی باشد");

    }
}