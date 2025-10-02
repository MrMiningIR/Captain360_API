using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Queries.GertByNoReceiverCompany;

public class GetCompanyManifestFormByNoReceiverCompanyQueryValidator : AbstractValidator<GetCompanyManifestFormByNoReceiverCompanyQuery>
{
    public GetCompanyManifestFormByNoReceiverCompanyQueryValidator()
    {
        RuleFor(x => x.No)
            .GreaterThan(0).WithMessage("شماره مانیفست باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.ReceiverCompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");
    }
}
