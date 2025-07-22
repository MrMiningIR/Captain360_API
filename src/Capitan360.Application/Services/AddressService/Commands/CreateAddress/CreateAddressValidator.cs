namespace Capitan360.Application.Services.AddressService.Commands.CreateAddress;

using FluentValidation;

public class CreateAddressValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressValidator()
    {
        RuleFor(x => x.AddressLine)
            .NotEmpty().WithMessage("AddressLine is required")
            .MaximumLength(200).WithMessage("AddressLine must not exceed 200 characters");

        RuleFor(x => x.Mobile)
            .Matches(@"^\+?\d{10,15}$").WithMessage("Mobile must be 10-15 digits")
            .When(x => x.Mobile != null);

        RuleFor(x => x.Tel1)
            .Matches(@"^\+?\d{10,15}$").WithMessage("Tel1 must be 10-15 digits")
            .When(x => x.Tel1 != null);

        RuleFor(x => x.Tel2)
            .Matches(@"^\+?\d{10,15}$").WithMessage("Tel2 must be 10-15 digits")
            .When(x => x.Tel2 != null);

        RuleFor(x => x.Zipcode)
            .Matches(@"^\d{5,10}$").WithMessage("Zipcode must be 5-10 digits")
            .When(x => x.Zipcode != null);
    }
}