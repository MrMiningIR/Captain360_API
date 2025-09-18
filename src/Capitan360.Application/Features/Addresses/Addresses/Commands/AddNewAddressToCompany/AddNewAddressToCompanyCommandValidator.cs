namespace Capitan360.Application.Features.Addresses.Addresses.Commands.AddNewAddressToCompany;

using FluentValidation;

public class AddNewAddressToCompanyCommandValidator : AbstractValidator<AddNewAddressToCompanyCommand>
{
    public AddNewAddressToCompanyCommandValidator()
    {
        RuleFor(x => x.AddressLine)
            .MaximumLength(200).WithMessage("خط آدرس نمی‌تواند بیشتر از 200 کاراکتر باشد")
            .NotEmpty()
            .WithMessage(" آدرس نمی‌تواند خالی باشد");

        RuleFor(x => x.Mobile)
            .Matches(@"^\+?\d{10,15}$").WithMessage("شماره موبایل باید بین 10 تا 11 رقم باشد")
            .When(x => x.Mobile != null);

        RuleFor(x => x.Tel1)
            .Matches(@"^\+?\d{10,15}$").WithMessage("شماره تلفن 1 باید بین 10 تا 11 رقم باشد")
            .When(x => x.Tel1 != null);

        RuleFor(x => x.Tel2)
            .Matches(@"^\+?\d{10,15}$").WithMessage("شماره تلفن 2 باید بین 10 تا 11 رقم باشد")
            .When(x => x.Tel2 != null);

        RuleFor(x => x.Zipcode)
            .Matches(@"^\d{3,10}$").WithMessage("کد پستی باید بین 3 تا 10 رقم باشد")
            .When(x => x.Zipcode != null);

        RuleFor(x => x.Description)
        .MaximumLength(200).WithMessage("خط آدرس نمی‌تواند بیشتر از 200 کاراکتر باشد")
        .NotEmpty()
        .WithMessage(" آدرس نمی‌تواند خالی باشد")
        .When(x => !string.IsNullOrEmpty(x.Description));
    }
}