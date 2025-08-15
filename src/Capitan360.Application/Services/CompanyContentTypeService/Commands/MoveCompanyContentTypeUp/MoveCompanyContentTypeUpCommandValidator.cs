using FluentValidation;

namespace Capitan360.Application.Services.CompanyContentTypeService.Commands.MoveCompanyContentTypeUp;

public class MoveCompanyContentTypeUpCommandValidator : AbstractValidator<MoveCompanyContentTypeUpCommand>
{
    public MoveCompanyContentTypeUpCommandValidator()
    {

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه محتوی باید بزرگ‌تر از صفر باشد");
    }
}