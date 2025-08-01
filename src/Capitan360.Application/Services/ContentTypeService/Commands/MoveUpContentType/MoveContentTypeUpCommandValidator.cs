using FluentValidation;

namespace Capitan360.Application.Services.ContentTypeService.Commands.MoveUpContentType;

public class MoveContentTypeUpCommandValidator : AbstractValidator<MoveContentTypeUpCommand>
{
    public MoveContentTypeUpCommandValidator()
    {

        RuleFor(x => x.CompanyTypeId)
     .GreaterThan(0).WithMessage("شناسه نوع شرکت باید بزرگ‌تر از صفر باشد");
        RuleFor(x => x.ContentTypeId)
            .GreaterThan(0).WithMessage("شناسه محتوی باید بزرگ‌تر از صفر باشد");
    }
}