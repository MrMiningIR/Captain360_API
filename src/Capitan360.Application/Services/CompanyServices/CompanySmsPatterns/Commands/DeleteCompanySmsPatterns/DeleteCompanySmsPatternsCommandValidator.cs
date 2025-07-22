using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanySmsPatterns.Commands.DeleteCompanySmsPatterns;

public class DeleteCompanySmsPatternsCommandValidator : AbstractValidator<DeleteCompanySmsPatternsCommand>
{
    public DeleteCompanySmsPatternsCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه الگوهای SMS باید بزرگ‌تر از صفر باشد");
    }
}