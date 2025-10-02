using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFromCompanyManifestForm;

public class BackCompanyDomesticWaybillFromCompanyManifestFormCommandValidator : AbstractValidator<BackCompanyDomesticWaybillFromCompanyManifestFormCommand>
{
    public BackCompanyDomesticWaybillFromCompanyManifestFormCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بارنامه بزرگتر از صفر باشد");
    }
}
