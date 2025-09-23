using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.DetachMasterWaybill;

public class DetachMasterWaybillCommandValidator : AbstractValidator<DetachMasterWaybillCommand>
{
    public DetachMasterWaybillCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه شناسایی مانفست بزرگتر از صفر باشد");
    }
}
