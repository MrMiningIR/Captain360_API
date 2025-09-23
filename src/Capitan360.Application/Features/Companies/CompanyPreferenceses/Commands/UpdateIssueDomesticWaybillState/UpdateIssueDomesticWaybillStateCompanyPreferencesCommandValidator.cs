using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.UpdateIssueDomesticWaybillState;

public class UpdateIssueDomesticWaybillStateCompanyPreferencesCommandValidator : AbstractValidator<UpdateIssueDomesticWaybillStateCompanyPreferencesCommand>
{
    public UpdateIssueDomesticWaybillStateCompanyPreferencesCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه تنظیمات باید مشخص باشد");
    }
}
