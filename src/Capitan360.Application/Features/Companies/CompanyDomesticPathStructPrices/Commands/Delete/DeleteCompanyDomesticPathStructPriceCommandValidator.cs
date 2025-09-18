using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyDomesticPathStructPrices.Commands.Delete;

public class DeleteCompanyDomesticPathStructPriceCommandValidator : AbstractValidator<DeleteCompanyDomesticPathStructPriceCommand>
{
    public DeleteCompanyDomesticPathStructPriceCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه قیمت ساختار مسیر باید بزرگ‌تر از صفر باشد");
    }
}