using Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.Delete;
using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.DeleteCompanyPreferences;

public class DeleteCompanyPreferencesCommandValidator : AbstractValidator<DeleteCompanyPreferencesCommand>
{
    public DeleteCompanyPreferencesCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه تنظیمات باید بزرگتر از صفر باشد");
    }
}