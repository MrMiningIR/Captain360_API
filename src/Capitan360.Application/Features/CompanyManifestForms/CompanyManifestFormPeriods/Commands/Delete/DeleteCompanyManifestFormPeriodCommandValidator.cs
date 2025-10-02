using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Commands.Delete;

public class DeleteCompanyManifestFormPeriodCommandValidator : AbstractValidator<DeleteCompanyManifestFormPeriodCommand>
{
    public DeleteCompanyManifestFormPeriodCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه دوره باید بزرگتر از صفر باشد");
    }
}