using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.UpdateDescription;

public class UpdateCompanyPackageTypeDescriptionCommandValidator : AbstractValidator<UpdateCompanyPackageTypeDescriptionCommand>
{
    public UpdateCompanyPackageTypeDescriptionCommandValidator()
    {
        RuleFor(x => x.CompanyPackageTypeDescription)
          .MaximumLength(500).WithMessage(" توضیحات بسته بندی نمی‌تواند بیشتر از 500 کاراکتر باشد")
          .MinimumLength(4).WithMessage(" توضیحات بسته بندی نمی‌تواند کمتر از 4 کاراکتر باشد")
          .When(x => !string.IsNullOrEmpty(x.CompanyPackageTypeDescription));
    }
}
