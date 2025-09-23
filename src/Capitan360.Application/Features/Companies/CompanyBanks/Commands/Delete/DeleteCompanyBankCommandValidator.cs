using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyBanks.Commands.Delete;

public class DeleteCompanyBankCommandValidator : AbstractValidator<DeleteCompanyBankCommand>
{
    public DeleteCompanyBankCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه بانک الزامی است");
    }
}
