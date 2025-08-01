using FluentValidation;

namespace Capitan360.Application.Services.PackageTypeService.Commands.UpdatePackageType;

//public class UpdatePackageTypeCommandValidator : AbstractValidator<UpdatePackageTypeCommand>
//{
//    public UpdatePackageTypeCommandValidator()
//    {
//        RuleFor(x => x.CompanyTypeId)
//            .GreaterThan(0).WithMessage("شناسه نوع شرکت باید بزرگ‌تر از صفر باشد");

//        RuleFor(x => x.PackageTypeName)
//            .NotEmpty().WithMessage("نام نوع محتوی نمی‌تواند خالی باشد")
//            .MaximumLength(50).WithMessage("نام نوع محتوی نمی‌تواند بیشتر از 50 کاراکتر باشد");

//        RuleFor(x => x.PackageTypeDescription)
//            .NotEmpty().WithMessage("توضیحات نوع محتوی نمی‌تواند خالی باشد")
//            .MaximumLength(50).WithMessage("توضیحات نوع محتوی نمی‌تواند بیشتر از 50 کاراکتر باشد");


//    }
//}

public class UpdatePackageTypeCommandValidator : BasePackageTypeValidator<UpdatePackageTypeCommand>
{
    public UpdatePackageTypeCommandValidator()
    {
        ApplyCommonRules(x => x.CompanyTypeId, x => x.PackageTypeName, x => x.PackageTypeDescription);

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه بسته بندی الزامی است و باید بزرگ‌تر از صفر باشد");
    }
}