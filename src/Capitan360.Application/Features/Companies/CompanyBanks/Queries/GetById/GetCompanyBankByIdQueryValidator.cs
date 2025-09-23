using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyBanks.Queries.GetById;

public class GetCompanyBankByIdQueryValidator : AbstractValidator<GetCompanyBankByIdQuery>
{
    public GetCompanyBankByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه بانک باید بزرگتر از صفر باشد");
    }
}
