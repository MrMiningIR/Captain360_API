using FluentValidation;

namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Delete;

public class DeleteCompanyInsuranceCommandValidator : AbstractValidator<DeleteCompanyInsuranceCommand>
{
    public DeleteCompanyInsuranceCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه شرکت بیمه باید بزرگتر از صفر باشد");
    }
}