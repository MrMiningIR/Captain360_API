using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.UpdateActiveState;

public class UpdateActiveStateCompanyDomesticPathCommandValidator : AbstractValidator<UpdateActiveStateCompanyDomesticPathCommand>
{
    public UpdateActiveStateCompanyDomesticPathCommandValidator()
    {
        RuleFor(x => x.Id)
        .GreaterThan(0).WithMessage("شناسه مسیر داخلی شرکت باید بزرگتر از صفر باشد");
    }
}