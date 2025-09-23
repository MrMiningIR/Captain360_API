using FluentValidation;

namespace Capitan360.Application.Features.ContentTypes.Commands.Delete;

public class DeleteContentTypeCommandValidator : AbstractValidator<DeleteContentTypeCommand>
{
    public DeleteContentTypeCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه محتوی بار باید بزرگتر از صفر باشد");
    }
}