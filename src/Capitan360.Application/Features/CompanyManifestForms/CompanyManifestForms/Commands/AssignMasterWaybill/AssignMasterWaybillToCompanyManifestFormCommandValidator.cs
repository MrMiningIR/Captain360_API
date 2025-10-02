using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.AssignMasterWaybill;

public class AssignMasterWaybillToCompanyManifestFormCommandValidator : AbstractValidator<AssignMasterWaybillToCompanyManifestFormCommand>
{
    public AssignMasterWaybillToCompanyManifestFormCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شماره شناسایی فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanySenderCaptain360Code)
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت فرستنده الزامی است")
            .MinimumLength(1).WithMessage("کد کاپیتان 360 شرکت فرستنده نمی‌تواند کمتر از 1 کاراکتر باشد");

        RuleFor(x => x.MasterWaybillNo)
           .NotNull().WithMessage("شماره بارنامه چاپ شرکت فرستنده نمی تواند خالی باشد است.")
           .MaximumLength(50).WithMessage("شماره بارنامه چاپ شرکت فرستنده نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.MasterWaybillWeight)
            .GreaterThan(0).WithMessage("وزن خالص باید بزرگتر از صفر باشد");

        RuleFor(x => x.MasterWaybillAirline)
           .NotNull().WithMessage("نام ایرلاین شرکت فرستنده نمی تواند خالی باشد است.")
           .MaximumLength(50).WithMessage("نام ایرلاین شرکت فرستنده نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.MasterWaybillFlightNo)
           .NotNull().WithMessage("شماره پرواز شرکت فرستنده نمی تواند خالی باشد است.")
           .MaximumLength(50).WithMessage("شماره پرواز شرکت فرستنده نمی‌تواند بیشتر از 50 کاراکتر باشد");
    }
}