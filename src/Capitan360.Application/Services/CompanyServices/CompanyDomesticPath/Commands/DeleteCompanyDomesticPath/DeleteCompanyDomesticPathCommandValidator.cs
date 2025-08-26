using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Commands.DeleteCompanyDomesticPath;

public class DeleteCompanyDomesticPathCommandValidator : AbstractValidator<DeleteCompanyDomesticPathCommand>
{
    public DeleteCompanyDomesticPathCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه مسیر داخلی شرکت باید بزرگتر از صفر باشد");
    }
}