using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.UpdateInternationalAirlineCargoStateCompanyPreferences
{
    public class UpdateInternationalAirlineCargoStateCompanyPreferencesCommandValidator : AbstractValidator<UpdateInternationalAirlineCargoStateCompanyPreferencesCommand>
    {
        public UpdateInternationalAirlineCargoStateCompanyPreferencesCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("شناسه تنظیمات باید مشخص باشد");
        }
    }
}
