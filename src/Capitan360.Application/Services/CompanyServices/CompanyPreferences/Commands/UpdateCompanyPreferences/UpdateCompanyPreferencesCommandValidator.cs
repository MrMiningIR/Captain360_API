using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.UpdateCompanyPreferences;

public class UpdateCompanyPreferencesCommandValidator : AbstractValidator<UpdateCompanyPreferencesCommand>
{
    public UpdateCompanyPreferencesCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه تنظیمات باید مشخص باشد");

        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد")
           ;

        RuleFor(x => x.EconomicCode)
            .NotEmpty().WithMessage("کد اقتصادی نمی‌تواند خالی باشد")
            .MaximumLength(50).WithMessage("کد اقتصادی نمی‌تواند بیشتر از 50 کاراکتر باشد")
            ;

        RuleFor(x => x.NationalId)
            .NotEmpty().WithMessage("شناسه ملی نمی‌تواند خالی باشد")
            .MaximumLength(50).WithMessage("شناسه ملی نمی‌تواند بیشتر از 50 کاراکتر باشد")
            ;

        RuleFor(x => x.RegistrationId)
            .NotEmpty().WithMessage("شناسه ثبت نمی‌تواند خالی باشد")
            .MaximumLength(50).WithMessage("شناسه ثبت نمی‌تواند بیشتر از 50 کاراکتر باشد")
            ;

        RuleFor(x => x.CaptainCargoName)
            .NotEmpty().WithMessage("نام کاپیتان کارگو نمی‌تواند خالی باشد")
            .MaximumLength(30).WithMessage("نام کاپیتان کارگو نمی‌تواند بیشتر از 30 کاراکتر باشد")
            ;
        RuleFor(x => x.CaptainCargoCode)
            .NotEmpty().WithMessage("کد کاپیتان کارگو نمی‌تواند خالی باشد")
            .MaximumLength(30).WithMessage("کد کاپیتان کارگو نمی‌تواند بیشتر از 30 کاراکتر باشد")
            ;
        RuleFor(x => x.Tax)
            .InclusiveBetween(0m, 999.99m).WithMessage("مالیات باید بین 0 و 999.99 باشد")
         ;
    }
}