using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.DeleteCompanyInsurance;

public class DeleteCompanyInsuranceCommandValidator : AbstractValidator<DeleteCompanyInsuranceCommand>
{
    public DeleteCompanyInsuranceCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه بیمه شرکت باید بزرگ‌تر از صفر باشد");
    }
}