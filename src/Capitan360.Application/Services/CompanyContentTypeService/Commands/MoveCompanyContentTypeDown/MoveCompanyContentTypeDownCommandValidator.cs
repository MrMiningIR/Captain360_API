using FluentValidation;

namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.MoveCompanyContentTypeDown;

public class MoveCompanyContentTypeDownCommandValidator : AbstractValidator<MoveCompanyContentTypeDownCommand>
{
    public MoveCompanyContentTypeDownCommandValidator()
    {

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه محتوی بار باید بزرگ‌تر از صفر باشد");
    }
}