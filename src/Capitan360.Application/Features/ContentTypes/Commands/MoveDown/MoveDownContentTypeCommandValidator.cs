using FluentValidation;

namespace Capitan360.Application.Features.ContentTypes.Commands.MoveDown;

public class MoveDownContentTypeCommandValidator : AbstractValidator<MoveDownContentTypeCommand>
{
    public MoveDownContentTypeCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه محتوی بار باید بزرگتر از صفر باشد");
    }
}