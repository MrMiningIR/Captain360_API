using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Commands.Delete;

public class DeleteCompanyDomesticWaybillPackageTypeCommandValidator : AbstractValidator<DeleteCompanyDomesticWaybillPackageTypeCommand>
{
    public DeleteCompanyDomesticWaybillPackageTypeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه بسته مرسوله باید بزرگتر از صفر باشد");
    }
}