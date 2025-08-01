using FluentValidation;

namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.MoveCompanyContentTypeDown;

public class MoveCompanyContentTypeDownCommandValidator : AbstractValidator<MoveCompanyContentTypeDownCommand>
{
    public MoveCompanyContentTypeDownCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");
        RuleFor(x => x.ContentTypeId)
            .GreaterThan(0).WithMessage("شناسه محتوی بار باید بزرگ‌تر از صفر باشد");
    }
}