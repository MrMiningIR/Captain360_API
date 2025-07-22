using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyPreferences.Commands.CreateCompanyPreferences;

public class CreateCompanyPreferencesValidator : AbstractValidator<CreateCompanyPreferencesCommand>
{
    public CreateCompanyPreferencesValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت الزامی است");

        RuleFor(x => x.EconomicCode)
            .NotEmpty().WithMessage("کد اقتصادی الزامی است")
            .MaximumLength(50).WithMessage("کد اقتصادی نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.NationalId)
            .NotEmpty().WithMessage("شناسه ملی الزامی است")
            .MaximumLength(50).WithMessage("شناسه ملی نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.RegistrationId)
            .NotEmpty().WithMessage("شناسه ثبت الزامی است")
            .MaximumLength(50).WithMessage("شناسه ثبت نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.CaptainCargoName)
            .NotEmpty().WithMessage("نام کاپیتان کارگو الزامی است")
            .MaximumLength(30).WithMessage("نام کاپیتان کارگو نمی‌تواند بیشتر از 30 کاراکتر باشد");

        RuleFor(x => x.CaptainCargoCode)
            .NotEmpty().WithMessage("کد کاپیتان کارگو الزامی است")
            .MaximumLength(30).WithMessage("کد کاپیتان کارگو نمی‌تواند بیشتر از 30 کاراکتر باشد");

        RuleFor(x => x.Tax)
            .InclusiveBetween(0m, 999.99m).WithMessage("مالیات باید بین 0 و 999.99 باشد");
    }
}