using FluentValidation;

namespace Capitan360.Application.Services.ContentTypeService.Commands.DeleteContentType;

public class DeleteContentTypeCommandValidator : AbstractValidator<DeleteContentTypeCommand>
{
    public DeleteContentTypeCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه محتوی بار باید بزرگتر از صفر باشد");
    }
}