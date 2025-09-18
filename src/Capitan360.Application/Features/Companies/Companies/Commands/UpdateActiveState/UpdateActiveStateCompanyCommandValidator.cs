using FluentValidation;

namespace Capitan360.Application.Features.Companies.Companies.Commands.UpdateActiveState;

public class UpdateActiveStateCompanyCommandValidator : AbstractValidator<UpdateActiveStateCompanyCommand>
{
    public UpdateActiveStateCompanyCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");
    }
}