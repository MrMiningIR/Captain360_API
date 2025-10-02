using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.DetachMasterWaybill;

public class DetachMasterWaybillFromCompanyManifestFormCommandValidator : AbstractValidator<DetachMasterWaybillFromCompanyManifestFormCommand>
{
    public DetachMasterWaybillFromCompanyManifestFormCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه شناسایی مانفست بزرگتر از صفر باشد");
    }
}
