using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.UpdateInternationalAirlineCargoState;

public class UpdateInternationalAirlineCargoStateCompanyPreferencesCommandValidator : AbstractValidator<UpdateInternationalAirlineCargoStateCompanyPreferencesCommand>
{
    public UpdateInternationalAirlineCargoStateCompanyPreferencesCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه تنظیمات باید مشخص باشد");
    }
}
