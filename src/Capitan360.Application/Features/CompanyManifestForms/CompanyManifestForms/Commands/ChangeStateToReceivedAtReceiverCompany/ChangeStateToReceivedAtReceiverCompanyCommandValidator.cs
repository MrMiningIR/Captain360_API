using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.ChangeStateToReceivedAtReceiverCompany;

public class ChangeStateToReceivedAtReceiverCompanyCommandValidator : AbstractValidator<ChangeStateToReceivedAtReceiverCompanyCommand>
{
    public ChangeStateToReceivedAtReceiverCompanyCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه فرم مانیسفست باید بزرگتر از صفر باشد");
    }
}