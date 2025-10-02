using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Queries.GetById;

public class GetCompanyManifestFormPeriodByIdQueryValidator : AbstractValidator<GetCompanyManifestFormPeriodByIdQuery>
{
    public GetCompanyManifestFormPeriodByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه مخزن باید بزرگتر از صفر باشد");
    }
}
