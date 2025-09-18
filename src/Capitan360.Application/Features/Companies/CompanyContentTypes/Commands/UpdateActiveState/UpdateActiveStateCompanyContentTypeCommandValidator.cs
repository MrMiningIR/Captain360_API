using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateActiveState;

public class UpdateActiveStateCompanyContentTypeCommandValidator : AbstractValidator<UpdateActiveStateCompanyContentTypeCommand>
{
    public UpdateActiveStateCompanyContentTypeCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه محتوی بار باید بزرگتر از صفر باشد");
    }
}