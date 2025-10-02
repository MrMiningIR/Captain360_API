using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Queries.GetByNoReceiverCompany;

public class GetCompanyDomesticWaybillByNoReceiverCompanyQueryValidator : AbstractValidator<GetCompanyDomesticWaybillByNoReceiverCompanyQuery>
{
    public GetCompanyDomesticWaybillByNoReceiverCompanyQueryValidator()
    {
        RuleFor(x => x.No)
            .GreaterThan(0).WithMessage("شماره بارنامه باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.ReceiverCompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");
    }
}
