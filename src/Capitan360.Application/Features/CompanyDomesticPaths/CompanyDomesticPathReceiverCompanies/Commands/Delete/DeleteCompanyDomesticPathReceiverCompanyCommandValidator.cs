using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Commands.Delete;

public class DeleteCompanyDomesticPathReceiverCompanyCommandValidator : AbstractValidator<DeleteCompanyDomesticPathReceiverCompanyCommand>
{
    public DeleteCompanyDomesticPathReceiverCompanyCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه مسیر داخلی شرکت باید بزرگتر از صفر باشد");
    }
}