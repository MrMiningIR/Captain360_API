using FluentValidation;

namespace Capitan360.Application.Services.ContentTypeService.Commands.MoveDownContentType;

public class MoveContentTypeDownCommandValidator : AbstractValidator<MoveContentTypeDownCommand>
{
    public MoveContentTypeDownCommandValidator()
    {

        RuleFor(x => x.ContentTypeId)
            .GreaterThan(0).WithMessage("شناسه محتوی باید بزرگ‌تر از صفر باشد");
    }
}