using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyBanks.Queries.GetByCompanyId;

public class GetCompanyBankByCompanyIdQueryValidator : AbstractValidator<GetCompanyBankByCompanyIdQuery>
{
    public GetCompanyBankByCompanyIdQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");
    }
}
