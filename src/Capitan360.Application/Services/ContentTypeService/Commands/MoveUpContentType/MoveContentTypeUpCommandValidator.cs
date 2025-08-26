using FluentValidation;

namespace Capitan360.Application.Services.ContentTypeService.Commands.MoveUpContentType;

public class MoveContentTypeUpCommandValidator : AbstractValidator<MoveContentTypeUpCommand>
{
    public MoveContentTypeUpCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه محتوی بار باید بزرگتر از صفر باشد");
    }
}