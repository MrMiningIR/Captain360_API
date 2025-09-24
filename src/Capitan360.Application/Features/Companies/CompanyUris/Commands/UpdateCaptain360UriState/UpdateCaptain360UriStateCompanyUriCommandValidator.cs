using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyUris.Commands.UpdateCaptain360UriState;

public class UpdateCaptain360UriStateCompanyUriCommandValidator : AbstractValidator<UpdateCaptain360UriStateCompanyUriCommand>
{
    public UpdateCaptain360UriStateCompanyUriCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه URI باید بزرگتر از صفر باشد");
    }
}