using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Queries.GertByIdReceiverCompany;

public class GetCompanyManifestFormByIdReceiverCompanyQueryValidator : AbstractValidator<GetCompanyManifestFormByIdReceiverCompanyQuery>
{
    public GetCompanyManifestFormByIdReceiverCompanyQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه فرم مانیفست باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.ReceiverCompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");
    }
}
