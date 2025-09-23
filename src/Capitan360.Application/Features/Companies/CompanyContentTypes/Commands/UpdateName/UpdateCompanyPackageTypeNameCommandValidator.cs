using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Commands.UpdateName;

internal class UpdateCompanyPackageTypeNameCommandValidator : AbstractValidator<UpdateCompanyPackageTypeNameCommand>
{
    public UpdateCompanyPackageTypeNameCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام الزامی است.")
            .MinimumLength(4).WithMessage("حداقل طول نام 4 کاراکتر است.")
            .MaximumLength(30).WithMessage("حداکثر طول نام 30 کاراکتر است.");
    }
}