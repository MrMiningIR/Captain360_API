using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.UpdateShowInSearchEngineState;

public class UpdateShowInSearchEngineStateCompanyPreferencesCommandValidator : AbstractValidator<UpdateShowInSearchEngineStateCompanyPreferencesCommand>
{
    public UpdateShowInSearchEngineStateCompanyPreferencesCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه تنظیمات باید مشخص باشد");
    }
}
