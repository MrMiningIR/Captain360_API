using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Commands.UpdateActiveState;

public class UpdateActiveStateManifestFormPeriodCommandValidator : AbstractValidator<UpdateActiveStateManifestFormPeriodCommand>
{
    public UpdateActiveStateManifestFormPeriodCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه دوره باید بزرگتر از صفر باشد");
    }
}