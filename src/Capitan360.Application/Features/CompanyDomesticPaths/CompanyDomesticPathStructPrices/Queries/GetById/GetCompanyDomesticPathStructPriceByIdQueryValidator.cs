using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Queries.GetById;

public class GetCompanyDomesticPathStructPriceByIdQueryValidator : AbstractValidator<GetCompanyDomesticPathStructPriceByIdQuery>
{
    public GetCompanyDomesticPathStructPriceByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه قیمت ساختار مسیر باید بزرگ‌تر از صفر باشد");
    }
}