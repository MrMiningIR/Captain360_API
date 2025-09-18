using FluentValidation;

namespace Capitan360.Application.Features.Companies.Companies.Commands.Delete;

public class DeleteCompanyCommandValidator : AbstractValidator<DeleteCompanyCommand>
{
    public DeleteCompanyCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");
    }
}