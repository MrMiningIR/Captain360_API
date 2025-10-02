using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Queries.GetByNoReceiverCompany;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Queries.GetByNoSenderCompany;

public class GetCompanyDomesticWaybillByNoSenderCompanyQueryValidator : AbstractValidator<GetCompanyDomesticWaybillByNoSenderCompanyQuery>
{
    public GetCompanyDomesticWaybillByNoSenderCompanyQueryValidator()
    {
        RuleFor(x => x.No)
            .GreaterThan(0).WithMessage("شماره بارنامه باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.SenderCompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");
    }
}