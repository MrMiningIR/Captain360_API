using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Commands.Delete;

public class DeleteCompanyDomesticPathStructPriceCommandValidator : AbstractValidator<DeleteCompanyDomesticPathStructPriceCommand>
{
    public DeleteCompanyDomesticPathStructPriceCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه قیمت ساختار مسیر باید بزرگ‌تر از صفر باشد");
    }
}