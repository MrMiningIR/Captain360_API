using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.UpdateWebServiceSearchEngineState;

public class UpdateWebServiceSearchEngineStateCompanyPreferencesCommandValidator : AbstractValidator<UpdateWebServiceSearchEngineStateCompanyPreferencesCommand>
{
    public UpdateWebServiceSearchEngineStateCompanyPreferencesCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه تنظیمات باید مشخص باشد");
    }
}
