using FluentValidation;

namespace Capitan360.Application.Features.Addresses.Addresses.Commands.Update;

public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه آدرس باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.MunicipalAreaId)
            .GreaterThan(0).WithMessage("منطقه شهرداری شرکت اجباری است");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180m, 180m).WithMessage("طول جغرافیایی معتبر نیست.");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-180m, 180m).WithMessage("عرض جغرافیایی معتبر نیست.");

        RuleFor(x => x.AddressLine)
            .NotNull().WithMessage("آدرس نمی تواند خالی باشد.")
            .MaximumLength(1000).WithMessage("آدرس نباید بیشتر از 1000 کاراکتر باشد");

        RuleFor(x => x.Mobile)
            .NotNull().WithMessage("شماره همراه نمی تواند خالی باشد.")
            .MaximumLength(11).WithMessage("شماره همراه نباید بیشتر از 11 کاراکتر باشد");

        RuleFor(x => x.Tel1)
            .NotNull().WithMessage("تلفن 1 نمی تواند خالی باشد.")
            .MaximumLength(30).WithMessage("تلفن 1 نباید بیشتر از 30 کاراکتر باشد");

        RuleFor(x => x.Tel2)
            .NotNull().WithMessage("تلفن 2 نمی تواند خالی باشد.")
            .MaximumLength(30).WithMessage("تلفن 2 نباید بیشتر از 30 کاراکتر باشد");

        RuleFor(x => x.Zipcode)
            .NotNull().WithMessage("کد پستی نمی تواند خالی باشد.")
            .MaximumLength(10).WithMessage("کد پستی نباید بیشتر از 10 کاراکتر باشد");

        RuleFor(x => x.Description)
            .NotNull().WithMessage("توضیحات نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("توضحیحات نباید بیشتر از 500 کاراکتر باشد");
    }
}