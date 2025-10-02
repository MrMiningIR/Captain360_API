using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Queries.GetByIdSenderCompany;

public class GetCompanyManifestFormByIdSenderCompanyQueryValidator : AbstractValidator<GetCompanyManifestFormByIdSenderCompanyQuery>
{
    public GetCompanyManifestFormByIdSenderCompanyQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه فرم مانیفست باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.SenderCompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");
    }
}
