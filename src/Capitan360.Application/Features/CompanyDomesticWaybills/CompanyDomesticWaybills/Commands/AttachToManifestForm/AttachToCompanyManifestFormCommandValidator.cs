using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.AttachToManifestForm;

public class AttachToCompanyManifestFormCommandValidator : AbstractValidator<AttachToCompanyManifestFormCommand>
{
    public AttachToCompanyManifestFormCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بارنامه بزرگتر از صفر باشد");

        RuleFor(x => x.CompanyManifestFormId)
           .GreaterThan(0).WithMessage("شناسه فرم مانیفست بزرگتر از صفر باشد");
    }
}
