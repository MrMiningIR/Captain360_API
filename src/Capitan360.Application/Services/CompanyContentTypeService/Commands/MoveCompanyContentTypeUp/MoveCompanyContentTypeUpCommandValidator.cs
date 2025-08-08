using FluentValidation;

namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.MoveCompanyContentTypeUp;

public class MoveCompanyContentTypeUpCommandValidator : AbstractValidator<MoveCompanyContentTypeUpCommand>
{
    public MoveCompanyContentTypeUpCommandValidator()
    {

        RuleFor(x => x.CompanyContentTypeId)
            .GreaterThan(0).WithMessage("شناسه محتوی باید بزرگ‌تر از صفر باشد");
    }
}