using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Queries.GetByIdSenderCompany;

public class GetCompanyDomesticWaybillByIdSenderCompanyQueryValidator : AbstractValidator<GetCompanyDomesticWaybillByIdSenderCompanyQuery>
{
    public GetCompanyDomesticWaybillByIdSenderCompanyQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه بارنامه باید بزرگ‌تر از صفر باشد");


        RuleFor(x => x.SenderCompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");
    }
}