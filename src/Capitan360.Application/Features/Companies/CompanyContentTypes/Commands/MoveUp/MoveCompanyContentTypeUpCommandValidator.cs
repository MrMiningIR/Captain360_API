using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.MoveUp;

public class MoveCompanyContentTypeUpCommandValidator : AbstractValidator<MoveCompanyContentTypeUpCommand>
{
    public MoveCompanyContentTypeUpCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه محتوی بار باید بزرگتر از صفر باشد");
    }
}