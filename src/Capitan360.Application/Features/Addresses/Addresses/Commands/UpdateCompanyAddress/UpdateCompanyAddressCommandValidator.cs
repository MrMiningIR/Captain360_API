using FluentValidation;

namespace Capitan360.Application.Features.Addresses.Addresses.Commands.Update;

public class UpdateCompanyAddressCommandValidator : AbstractValidator<UpdateCompanyAddressCommand>
{
    public UpdateCompanyAddressCommandValidator()
    {
        RuleFor(x => x.AddressId)
            .GreaterThan(0).WithMessage("شناسه آدرس باید مشخص باشد");

        RuleFor(x => x.AddressLine)
            .NotEmpty().WithMessage("خط آدرس نمی‌تواند خالی باشد")
            .MaximumLength(200).WithMessage("خط آدرس نمی‌تواند بیشتر از 200 کاراکتر باشد")
            .When(x => x.AddressLine != null);

        RuleFor(x => x.Mobile)
            .Matches(@"^\+?\d{10,15}$").WithMessage("شماره موبایل باید بین 10 تا 15 رقم باشد")
            .When(x => x.Mobile != null);

        RuleFor(x => x.Tel1)
            .Matches(@"^\+?\d{10,15}$").WithMessage("شماره تلفن 1 باید بین 10 تا 15 رقم باشد")
            .When(x => x.Tel1 != null);

        RuleFor(x => x.Tel2)
            .Matches(@"^\+?\d{10,15}$").WithMessage("شماره تلفن 2 باید بین 10 تا 15 رقم باشد")
            .When(x => x.Tel2 != null);

        RuleFor(x => x.Zipcode)
            .Matches(@"^\d{5,10}$").WithMessage("کد پستی باید بین 5 تا 10 رقم باشد")
            .When(x => x.Zipcode != null);
    }
}