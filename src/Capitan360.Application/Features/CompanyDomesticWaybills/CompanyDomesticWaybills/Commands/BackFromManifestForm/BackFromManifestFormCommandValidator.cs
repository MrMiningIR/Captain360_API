using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFromManifestForm;

public class BackFromManifestFormCommandValidator : AbstractValidator<BackFromManifestFormCommand>
{
    public BackFromManifestFormCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بارنامه بزرگتر از صفر باشد");
    }
}
