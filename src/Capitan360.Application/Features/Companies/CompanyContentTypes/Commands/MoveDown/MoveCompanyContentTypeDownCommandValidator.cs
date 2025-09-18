using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.MoveDown;

public class MoveCompanyContentTypeDownCommandValidator : AbstractValidator<MoveCompanyContentTypeDownCommand>
{
    public MoveCompanyContentTypeDownCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه محتوی بار باید بزرگتر از صفر باشد");
    }
}