namespace Capitan360.Application.Services.AddressService.Commands.UpdateAddress;

using FluentValidation;

public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
    {
        //RuleFor(x => x.Id)
        //    .GreaterThan(0).WithMessage("شناسه آدرس باید مشخص باشد");

        RuleFor(x => x.AddressLine)
            .NotEmpty().WithMessage("AddressLine cannot be empty")
            .MaximumLength(200).WithMessage("AddressLine must not exceed 200 characters")
            .When(x => x.AddressLine != null);

        RuleFor(x => x.Mobile)
            .Matches(@"^\+?\d{11}$").WithMessage("Mobile must be 11 digits")
            .When(x => x.Mobile != null);

        RuleFor(x => x.Tel1)
            .Matches(@"^\+?\d{11}$").WithMessage("Tel1 must be 11 digits")
            .When(x => x.Tel1 != null);

        RuleFor(x => x.Tel2)
            .Matches(@"^\+?\d{11}$").WithMessage("Tel2 must be 11 digits")
            .When(x => x.Tel2 != null);

        RuleFor(x => x.Zipcode)
            .Matches(@"^\d{3,10}$").WithMessage("Zipcode must be 3-10 digits")
            .When(x => x.Zipcode != null);
    }
}