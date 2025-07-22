using FluentValidation;

namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.MoveCompanyContentTypeDown;

public class MoveCompanyContentTypeDownValidator : AbstractValidator<MoveCompanyContentTypeDownCommand>
{
    public MoveCompanyContentTypeDownValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه Company باید بزرگ‌تر از صفر باشد");
        RuleFor(x => x.ContentTypeId)
            .GreaterThan(0).WithMessage("شناسه ContentType باید بزرگ‌تر از صفر باشد");
    }
}