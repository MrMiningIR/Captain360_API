using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Commands.DeleteByCompanyDomesticWaybillId;

public class DeleteDomesticWaybillPackageTypeByDomesticWaybillIdCommandValidator : AbstractValidator<DeleteDomesticWaybillPackageTypeByDomesticWaybillIdCommand>
{
    public DeleteDomesticWaybillPackageTypeByDomesticWaybillIdCommandValidator()
    {
        RuleFor(x => x.CompanyDomesticWaybillId)
            .GreaterThan(0).WithMessage("شناسه بارنامه باید بزرگتر از صفر باشد");
    }
}