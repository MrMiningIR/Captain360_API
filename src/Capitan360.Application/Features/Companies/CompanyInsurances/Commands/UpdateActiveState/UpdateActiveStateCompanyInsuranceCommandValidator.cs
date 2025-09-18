using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyInsurances.Commands.UpdateActiveState;

public class UpdateActiveStateCompanyInsuranceCommandValidator : AbstractValidator<UpdateActiveStateCompanyInsuranceCommand>
{
    public UpdateActiveStateCompanyInsuranceCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه شرکت بیمه باید بزرگتر از صفر باشد");
    }
}