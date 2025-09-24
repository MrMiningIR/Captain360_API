using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyUris.Commands.UpdateActiveState;

public class UpdateActiveStateCompanyUriCommandValidator : AbstractValidator<UpdateActiveStateCompanyUriCommand>
{
    public UpdateActiveStateCompanyUriCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه URI باید بزرگتر از صفر باشد");
    }
}
