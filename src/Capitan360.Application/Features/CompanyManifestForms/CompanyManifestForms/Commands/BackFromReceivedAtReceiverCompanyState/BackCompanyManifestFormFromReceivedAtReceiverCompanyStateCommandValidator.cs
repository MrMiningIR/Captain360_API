using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.BackFromReceivedAtReceiverCompanyState;

public class BackCompanyManifestFormFromReceivedAtReceiverCompanyStateCommandValidator : AbstractValidator<BackCompanyManifestFormFromReceivedAtReceiverCompanyStateCommand>
{
    public BackCompanyManifestFormFromReceivedAtReceiverCompanyStateCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه فرم مانیسفست باید بزرگتر از صفر باشد");
    }
}