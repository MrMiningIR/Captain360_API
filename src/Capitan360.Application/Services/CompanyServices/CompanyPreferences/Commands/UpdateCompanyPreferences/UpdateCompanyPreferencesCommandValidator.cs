using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.UpdateCompanyPreferences;

public class UpdateCompanyPreferencesCommandValidator : AbstractValidator<UpdateCompanyPreferencesCommand>
{
    public UpdateCompanyPreferencesCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه تنظیمات باید مشخص باشد");

        RuleFor(x => x.EconomicCode)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.EconomicCode))
            .WithMessage("کد اقتصادی نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.NationalId)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.NationalId))
            .WithMessage("شناسه ملی نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.RegistrationId)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.RegistrationId))
            .WithMessage("شناسه ثبت نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.CaptainCargoName)
            .MaximumLength(30)
            .When(x => !string.IsNullOrWhiteSpace(x.CaptainCargoName))
            .WithMessage("نام کاپیتان کارگو نمی‌تواند بیشتر از 30 کاراکتر باشد");

        RuleFor(x => x.CaptainCargoCode)
            .MaximumLength(30)
            .When(x => !string.IsNullOrWhiteSpace(x.CaptainCargoCode))
            .WithMessage("کد کاپیتان کارگو نمی‌تواند بیشتر از 30 کاراکتر باشد");

        RuleFor(x => x.Tax)
            .InclusiveBetween(0m, 100.00m).WithMessage("مالیات باید بین 0 و 100.00 باشد");
    }
}