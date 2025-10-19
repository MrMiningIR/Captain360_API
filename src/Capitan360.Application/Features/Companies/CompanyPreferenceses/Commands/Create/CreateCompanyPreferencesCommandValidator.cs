using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.Create;

public class CreateCompanyPreferencesCommandValidator : AbstractValidator<CreateCompanyPreferencesCommand>
{
    public CreateCompanyPreferencesCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت الزامی است");

        RuleFor(x => x.EconomicCode)
            .NotNull().WithMessage("کد اقتصادی نمی تواند خالی باشد.")
            .MaximumLength(50).WithMessage("کد اقتصادی نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.NationalId)
            .NotNull().WithMessage("شناسه ملی نمی تواند خالی باشد.")
            .MaximumLength(50).WithMessage("شناسه ملی نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.RegistrationId)
            .NotNull().WithMessage("شناسه ثبت نمی تواند خالی باشد.")
            .MaximumLength(50).WithMessage("شناسه ثبت نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.CaptainCargoName)
            .NotEmpty().WithMessage("نام کاپیتان کارگو الزامی است.")
            .MaximumLength(30).WithMessage("نام کاپیتان کارگو نمی‌تواند بیشتر از 30 کاراکتر باشد");

        RuleFor(x => x.CaptainCargoCode)
            .NotEmpty().WithMessage("کد کاپیتان کارگو الزامی است.")
            .MaximumLength(10).WithMessage("کد کاپیتان کارگو نمی‌تواند بیشتر از 10 کاراکتر باشد");

        RuleFor(x => x.Tax)
            .InclusiveBetween(0m, 100m).WithMessage("درصد مالیات باید بین 0 تا 100 باشد.")
            .PrecisionScale(5, 2, true).WithMessage("درصد مالیات باید حداکثر 2 رقم اعشار و مجموعاً حداکثر 5 رقم داشته باشد.");
    }
}