using FluentValidation;

namespace Capitan360.Application.Services.ContentTypeService.Commands.MoveUpContentType;

public class MoveContentTypeUpValidator : AbstractValidator<MoveContentTypeUpCommand>
{
    public MoveContentTypeUpValidator()
    {

        RuleFor(x => x.CompanyTypeId)
            .GreaterThan(0).WithMessage("شناسه CompanyType باید بزرگ‌تر از صفر باشد");
        RuleFor(x => x.ContentTypeId)
            .GreaterThan(0).WithMessage("شناسه ContentType باید بزرگ‌تر از صفر باشد");
    }
}