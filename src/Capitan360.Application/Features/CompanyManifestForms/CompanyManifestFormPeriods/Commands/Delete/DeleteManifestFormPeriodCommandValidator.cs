using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Commands.Delete;

public class DeleteManifestFormPeriodCommandValidator : AbstractValidator<DeleteManifestFormPeriodCommand>
{
    public DeleteManifestFormPeriodCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");
    }
}