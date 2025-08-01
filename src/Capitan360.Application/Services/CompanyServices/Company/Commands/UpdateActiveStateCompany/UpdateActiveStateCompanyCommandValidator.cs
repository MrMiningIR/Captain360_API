using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.Company.Commands.UpdateActiveStateCompany;

public class UpdateActiveStateCompanyCommandValidator : AbstractValidator<UpdateActiveStateCompanyCommand>
{


    public UpdateActiveStateCompanyCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگ‌تر از صفر باشد");
    }
}