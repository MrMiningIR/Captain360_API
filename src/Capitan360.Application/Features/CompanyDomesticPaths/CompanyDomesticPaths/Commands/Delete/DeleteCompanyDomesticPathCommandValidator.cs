using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Delete;

public class DeleteCompanyDomesticPathCommandValidator : AbstractValidator<DeleteCompanyDomesticPathCommand>
{
    public DeleteCompanyDomesticPathCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه مسیر داخلی باید بزرگتر از صفر باشد");
    }
}