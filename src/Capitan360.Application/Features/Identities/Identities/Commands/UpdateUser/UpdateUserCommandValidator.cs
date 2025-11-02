using Capitan360.Domain.Enums;
using FluentValidation;

namespace Capitan360.Application.Features.Identities.Identities.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{


    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.NameFamily)
            .NotEmpty()
            .WithMessage("NameFamily is required")
            .MaximumLength(100)
            .WithMessage("NameFamily must not exceed 100 characters")
            .MinimumLength(5)
            .WithMessage("NameFamily must be at least 5 characters");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("PhoneNumber is required")
            .Matches(@"^\d{11}$")
            .WithMessage("PhoneNumber must be 11 digits");



        RuleFor(x => x.TypeOfFactorInSamanehMoadianId)
            .Must(moadianType => Enum.IsDefined(typeof(MoadianFactorType), moadianType))
            .WithMessage($"Invalid TypeOfFactorInSamanehMoadianId. Valid values are: {string.Join(", ", Enum.GetValues(typeof(MoadianFactorType)))}");


        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("ایمیل باید به فرمت معتبر باشد");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("شناسه کاربری نمیتواند خالی باشد");

    }
}