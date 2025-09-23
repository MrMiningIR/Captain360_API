using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.BackFromReceivedAtReceiverCompanyState;

public class BackFromReceivedAtReceiverCompanyStateCommandValidator : AbstractValidator<BackFromReceivedAtReceiverCompanyStateCommand>
{
    public BackFromReceivedAtReceiverCompanyStateCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه فرم مانیسفست باید بزرگتر از صفر باشد");
    }
}