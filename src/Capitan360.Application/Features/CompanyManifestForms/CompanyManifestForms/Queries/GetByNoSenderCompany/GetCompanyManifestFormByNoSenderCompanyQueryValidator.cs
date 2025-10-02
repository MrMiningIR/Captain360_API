using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Queries.GetByNoSenderCompany;

public class GetCompanyManifestFormByNoSenderCompanyQueryValidator : AbstractValidator<GetCompanyManifestFormByNoSenderCompanyQuery>
{
    public GetCompanyManifestFormByNoSenderCompanyQueryValidator()
    {
        RuleFor(x => x.No)
            .GreaterThan(0).WithMessage("شماره مانیفست باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.SenderCompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");
    }
}
