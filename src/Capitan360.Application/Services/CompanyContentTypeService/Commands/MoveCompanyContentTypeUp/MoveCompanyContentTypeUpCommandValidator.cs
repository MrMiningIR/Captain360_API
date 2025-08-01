using FluentValidation;

namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.MoveCompanyContentTypeUp;

public class MoveCompanyContentTypeUpCommandValidator : AbstractValidator<MoveCompanyContentTypeUpCommand>
{
    public MoveCompanyContentTypeUpCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");
        RuleFor(x => x.ContentTypeId)
            .GreaterThan(0).WithMessage("شناسه محتوی باید بزرگ‌تر از صفر باشد");
    }
}