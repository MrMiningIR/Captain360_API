using FluentValidation;

namespace Capitan360.Application.Features.ContentTypes.Commands.MoveUp;

public class MoveUpContentTypeCommandValidator : AbstractValidator<MoveUpContentTypeCommand>
{
    public MoveUpContentTypeCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه محتوی بار باید بزرگتر از صفر باشد");
    }
}