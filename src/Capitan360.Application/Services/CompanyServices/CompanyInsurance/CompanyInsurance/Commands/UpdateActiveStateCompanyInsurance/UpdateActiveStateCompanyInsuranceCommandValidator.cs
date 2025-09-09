using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.UpdateActiveStateCompanyInsurance;

public class UpdateActiveStateCompanyInsuranceCommandValidator : AbstractValidator<UpdateActiveStateCompanyInsuranceCommand>
{
    public UpdateActiveStateCompanyInsuranceCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه شرکت بیمه باید بزرگتر از صفر باشد");
    }
}