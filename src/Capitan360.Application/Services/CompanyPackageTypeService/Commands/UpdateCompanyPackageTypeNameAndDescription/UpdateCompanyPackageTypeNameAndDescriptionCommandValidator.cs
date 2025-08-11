using FluentValidation;

namespace Capitan360.Application.Services.CompanyPackageTypeService.Commands.UpdateCompanyPackageTypeNameAndDescription;

public class UpdateCompanyPackageTypeNameAndDescriptionCommandValidator : AbstractValidator<UpdateCompanyPackageTypeNameAndDescriptionCommand>
{
    public UpdateCompanyPackageTypeNameAndDescriptionCommandValidator()
    {
        RuleFor(x => x.Id)
   .GreaterThan(0).WithMessage("شناسه نوع بسته بندی شرکت الزامی است");


        RuleFor(x => x.CompanyPackageTypeName)
             .NotEmpty().WithMessage("نام بسته بندی الزامی است")
            .MaximumLength(50).WithMessage("نام بسته بندی نمی‌تواند بیشتر از 50 کاراکتر باشد")
            .MinimumLength(4).WithMessage("نام بسته بندی نمی‌تواند کمتر از 4 کاراکتر باشد");
        RuleFor(x => x.CompanyPackageTypeDescription)
       .MaximumLength(500).WithMessage(" توضیحات بسته بندی نمی‌تواند بیشتر از 500 کاراکتر باشد")
       .MinimumLength(4).WithMessage(" توضیحات بسته بندی نمی‌تواند کمتر از 4 کاراکتر باشد")
       .When(x => !string.IsNullOrEmpty(x.CompanyPackageTypeDescription));
    }
}
