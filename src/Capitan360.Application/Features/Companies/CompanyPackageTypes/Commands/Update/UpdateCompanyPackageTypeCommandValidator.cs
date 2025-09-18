using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Commands.Update;

public class UpdateCompanyPackageTypeCommandValidator : AbstractValidator<UpdateCompanyPackageTypeCommand>
{
    public UpdateCompanyPackageTypeCommandValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("شناسه بسته بندی بار باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanyPackageTypeName)
            .NotEmpty().WithMessage("نام بسته بندی بار الزامی است")
            .MinimumLength(4).WithMessage("نام بسته بندی بار نمی‌تواند کمتر از 4 کاراکتر باشد")
            .MaximumLength(50).WithMessage("نام بسته بندی بار نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.CompanyPackageTypeDescription)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyPackageTypeDescription))
            .WithMessage("توضیحات بسته بندی بار نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}