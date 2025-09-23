using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.BackFromAirlineDeliveryState;

public class BackFromAirlineDeliveryStateCommandValidator : AbstractValidator<BackFromAirlineDeliveryStateCommand>
{
    public BackFromAirlineDeliveryStateCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه فرم مانیسفست باید بزرگتر از صفر باشد");
    }
}