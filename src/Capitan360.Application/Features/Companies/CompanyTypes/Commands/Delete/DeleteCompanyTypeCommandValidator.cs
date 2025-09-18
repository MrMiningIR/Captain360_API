using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyType.Commands.DeleteCompanyType;

public class DeleteCompanyTypeCommandValidator : AbstractValidator<DeleteCompanyTypeCommand>
{
    public DeleteCompanyTypeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت باید بزرگتر از صفر باشد");
    }
}