using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.DeleteCompanyCommissions;

public class DeleteCompanyCommissionsCommandValidator : AbstractValidator<DeleteCompanyCommissionsCommand>
{
    public DeleteCompanyCommissionsCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه کمیسیون باید بزرگ‌تر از صفر باشد");
    }
}