using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.DeleteCompanyInsuranceCharge;

public class DeleteCompanyInsuranceChargeCommandValidator : AbstractValidator<DeleteCompanyInsuranceChargeCommand>
{
    public DeleteCompanyInsuranceChargeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه شارژ بیمه شرکت باید بزرگ‌تر از صفر باشد");
    }
}