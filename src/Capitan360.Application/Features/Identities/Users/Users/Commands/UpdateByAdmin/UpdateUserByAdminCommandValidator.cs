using Capitan360.Domain.Enums;
using FluentValidation;

namespace Capitan360.Application.Features.Identities.Users.Users.Commands.UpdateByAdmin;

public class UpdateUserByAdminCommandValidator : AbstractValidator<UpdateUserByAdminCommand>
{
    public UpdateUserByAdminCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("شناسه کاربر الزامی است")
            .MaximumLength(450).WithMessage("شناسه کاربر نمی‌تواند بیشتر از 450 کاراکتر باشد");

        RuleFor(x => x.NameFamily)
            .NotNull().WithMessage("نام و نام خانوادگی نمی تواند خالی باشد.")
            .MaximumLength(100).WithMessage("نام و نام خانوادگی نباید بیشتر از 100 کاراکتر باشد");

        RuleFor(x => x.AccountCodeInDesktopCaptainCargo)
            .NotNull().WithMessage("کد کاپیتان کارگو نمی تواند خالی باشد.")
            .MaximumLength(50).WithMessage("کد کاپیتان کارگو نباید بیشتر از 50 کاراکتر باشد");

        RuleFor(x => (int)x.TypeOfFactorInSamanehMoadianId)
           .Must(typeOfFactorInSamanehMoadianId => Enum.IsDefined(typeof(MoadianFactorType), typeOfFactorInSamanehMoadianId))
           .WithMessage("نوع سامانه مودیان معتبر نیست");
        When(x => x.TypeOfFactorInSamanehMoadianId == (short)MoadianFactorType.Hoghoghi, () =>
        {
            RuleFor(x => x.EconomicCode)
                .NotEmpty().WithMessage("کد اقتصادی برای حقوقی الزامی است")
                .MaximumLength(50).WithMessage("کد اقتصادی نمی‌تواند بیشتر از 50 کاراکتر باشد");

            RuleFor(x => x.NationalId)
                .NotEmpty().WithMessage("شناسه ملی برای حقوقی الزامی است")
                .MaximumLength(50).WithMessage("شناسه ملی برای حقوقی نمی‌تواند بیشتر از 50 کاراکتر باشد");

            RuleFor(x => x.RegistrationId)
                .NotNull().WithMessage("شناسه ثبت برای حقوقی الزامی است")
                .MaximumLength(50).WithMessage("شناسه ثبت برای حقوقی نمی‌تواند بیشتر از 50 کاراکتر باشد");
        });
        When(x => x.TypeOfFactorInSamanehMoadianId == (short)MoadianFactorType.Haghigh, () =>
        {
            RuleFor(x => x.NationalCode)
                .NotEmpty().WithMessage("کد ملی برای حقیقی الزامی است")
                .MaximumLength(50).WithMessage("کد ملی نمی‌تواند بیشتر از 50 کاراکتر باشد");
        });

        RuleFor(x => x.MobileTelegram)
            .NotEmpty().WithMessage("شماره موبایل تلگرام اجباری است")
            .Length(11).WithMessage("طول موبایل تلگرام باید دقیقاً 11 رقم باشد.")
            .Matches(@"(^(0?9)|(\+?989))\d{2}\W?\d{3}\W?\d{4}").WithMessage("شماره موبایل تلگرام معتبر نیست");

        RuleFor(x => x.Tell)
            .NotNull().WithMessage("تلفن نمی تواند خالی باشد.")
            .MaximumLength(30).WithMessage("تلفن نباید بیشتر از 30 کاراکتر باشد");

        RuleFor(x => x.Email)
            .MaximumLength(256).WithMessage("طول ایمیل باید دقیقاً 256 رقم باشد.")
            .EmailAddress().WithMessage("شماره موبایل تلگرام معتبر نیست");

        RuleFor(x => x.Email)
            .NotNull().WithMessage("ایمیل نمی تواند خالی باشد.")
            .MaximumLength(256)
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("حداکثر طول ایمیل باید 256 کاراکتر باشد.")
            .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("فرمت ایمیل معتبر نیست.");

        RuleFor(x => x.Description)
            .NotNull().WithMessage("توضیحات نمی تواند خالی باشد.")
            .MaximumLength(500).WithMessage("توضحیحات نباید بیشتر از 500 کاراکتر باشد");

    }
}
