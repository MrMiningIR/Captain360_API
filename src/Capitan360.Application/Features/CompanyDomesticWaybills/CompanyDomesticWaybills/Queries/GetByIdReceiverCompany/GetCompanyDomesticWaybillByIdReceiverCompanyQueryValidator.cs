using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Queries.GetByIdReceiverCompany;

public class GetCompanyDomesticWaybillByIdReceiverCompanyQueryValidator : AbstractValidator<GetCompanyDomesticWaybillByIdReceiverCompanyQuery>
{
    public GetCompanyDomesticWaybillByIdReceiverCompanyQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه بارنامه باید بزرگ‌تر از صفر باشد");

        RuleFor(x => x.ReceiverCompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");
    }
}
