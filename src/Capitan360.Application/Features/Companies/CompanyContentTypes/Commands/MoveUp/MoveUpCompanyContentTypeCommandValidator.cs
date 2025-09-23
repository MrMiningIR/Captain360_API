using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.MoveUp;

public class MoveUpCompanyContentTypeCommandValidator : AbstractValidator<MoveUpCompanyContentTypeCommand>
{
    public MoveUpCompanyContentTypeCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه محتوی بار باید بزرگتر از صفر باشد");
    }
}