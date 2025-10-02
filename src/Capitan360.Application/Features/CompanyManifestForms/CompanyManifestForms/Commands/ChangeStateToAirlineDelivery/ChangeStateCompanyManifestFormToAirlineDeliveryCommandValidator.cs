using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.ChangeStateToAirlineDelivery;

public class ChangeStateCompanyManifestFormToAirlineDeliveryCommandValidator : AbstractValidator<ChangeStateCompanyManifestFormToAirlineDeliveryCommand>
{
    public ChangeStateCompanyManifestFormToAirlineDeliveryCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه فرم مانیسفست باید بزرگتر از صفر باشد");
    }
}