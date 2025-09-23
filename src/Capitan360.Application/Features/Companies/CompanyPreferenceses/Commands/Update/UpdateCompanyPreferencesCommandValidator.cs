using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.Update;

public class UpdateCompanyPreferencesCommandValidator : AbstractValidator<UpdateCompanyPreferencesCommand>
{
    public UpdateCompanyPreferencesCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه تنظیمات باید مشخص باشد");

        RuleFor(x => x.EconomicCode)
            .NotEmpty().WithMessage("کد اقتصادی الزامی است.")
            .MaximumLength(50).WithMessage("کد اقتصادی نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.NationalId)
            .NotEmpty().WithMessage("شناسه ملی الزامی است.")
            .MaximumLength(50).WithMessage("شناسه ملی نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.RegistrationId)
            .NotEmpty().WithMessage("شناسه ثبت الزامی است.")
            .MaximumLength(50).WithMessage("شناسه ثبت نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.CaptainCargoName)
            .NotEmpty().WithMessage("نام کاپیتان کارگو الزامی است.")
            .MaximumLength(30).WithMessage("نام کاپیتان کارگو نمی‌تواند بیشتر از 30 کاراکتر باشد");

        RuleFor(x => x.CaptainCargoCode)
            .NotEmpty().WithMessage("کد کاپیتان کارگو الزامی است.")
            .MaximumLength(30).WithMessage("کد کاپیتان کارگو نمی‌تواند بیشتر از 30 کاراکتر باشد");

        RuleFor(x => x.Tax)
            .InclusiveBetween(0m, 100m).WithMessage("مالیات باید بین 0 تا 100 باشد.")
            .PrecisionScale(5, 2, true).WithMessage("Tax باید حداکثر 2 رقم اعشار و مجموعاً حداکثر 5 رقم داشته باشد.");
    }
}