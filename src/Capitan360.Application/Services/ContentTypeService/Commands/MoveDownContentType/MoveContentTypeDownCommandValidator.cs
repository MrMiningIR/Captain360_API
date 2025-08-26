using FluentValidation;

namespace Capitan360.Application.Services.ContentTypeService.Commands.MoveDownContentType;

public class MoveContentTypeDownCommandValidator : AbstractValidator<MoveContentTypeDownCommand>
{
    public MoveContentTypeDownCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه محتوی بار باید بزرگتر از صفر باشد");
    }
}