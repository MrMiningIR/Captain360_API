using FluentValidation;

namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsuranceCharges.Commands.Create;

public class CreateCompanyInsuranceChargeCommandValidator : AbstractValidator<CreateCompanyInsuranceChargeCommand>
{
    public CreateCompanyInsuranceChargeCommandValidator()
    {
        RuleFor(x => x.Rate)
            .IsInEnum().WithMessage("نرخ نامعتبر است");

        RuleFor(x => x.Value)
            .GreaterThanOrEqualTo(0).WithMessage("مقدار باید بزرگ‌تر یا برابر با صفر باشد");

        RuleFor(x => x.Settlement)
            .GreaterThanOrEqualTo(0).WithMessage("تسویه باید بزرگ‌تر یا برابر با صفر باشد");

        RuleFor(x => x.CompanyInsuranceId)
            .GreaterThan(0).WithMessage("شناسه بیمه شرکت الزامی است");
    }
}