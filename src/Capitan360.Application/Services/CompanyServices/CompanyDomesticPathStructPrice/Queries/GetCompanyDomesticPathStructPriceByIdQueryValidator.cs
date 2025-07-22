using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Queries;

public class GetCompanyDomesticPathStructPriceByIdQueryValidator : AbstractValidator<GetCompanyDomesticPathStructPriceByIdQuery>
{
    public GetCompanyDomesticPathStructPriceByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه قیمت ساختار مسیر باید بزرگ‌تر از صفر باشد");
    }
}