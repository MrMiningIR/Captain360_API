using FluentValidation;

namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.MoveCompanyContentTypeUp;

public class MoveCompanyContentTypeUpValidator : AbstractValidator<MoveCompanyContentTypeUpCommand>
{
    public MoveCompanyContentTypeUpValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه Company باید بزرگ‌تر از صفر باشد");
        RuleFor(x => x.ContentTypeId)
            .GreaterThan(0).WithMessage("شناسه ContentType باید بزرگ‌تر از صفر باشد");
    }
}