using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyUri.Commands.DeleteCompanyUri;

public class DeleteCompanyUriCommandValidator : AbstractValidator<DeleteCompanyUriCommand>
{
    public DeleteCompanyUriCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه URI باید بزرگتر از صفر باشد");
    }
}