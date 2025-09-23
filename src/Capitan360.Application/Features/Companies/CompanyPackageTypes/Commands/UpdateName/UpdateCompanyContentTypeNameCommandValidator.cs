using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateName;

public class UpdateCompanyContentTypeNameCommandValidator : AbstractValidator<UpdateCompanyContentTypeNameCommand>
{
    public UpdateCompanyContentTypeNameCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام الزامی است.")
            .MinimumLength(4).WithMessage("حداقل طول نام 4 کاراکتر است.")
            .MaximumLength(30).WithMessage("حداکثر طول نام 30 کاراکتر است.");
    }
}