using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.BackFromAirlineDeliveryState;

public class BackCompanyManifestFormFromAirlineDeliveryStateCommandValidator : AbstractValidator<BackCompanyManifestFormFromAirlineDeliveryStateCommand>
{
    public BackCompanyManifestFormFromAirlineDeliveryStateCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه فرم مانیسفست باید بزرگتر از صفر باشد");
    }
}