using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateName;

public class UpdateCompanyPackageTypeNameCommandValidator : AbstractValidator<UpdateCompanyPackageTypeNameCommand>
{
    public UpdateCompanyPackageTypeNameCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگتر از صفر باشد");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام الزامی است.")
            .MaximumLength(30).WithMessage("حداکثر طول نام 30 کاراکتر است.");
    }
}