using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.AssignMasterWaybillFromDesktop;

public class AssignMasterWaybillFromDesktopCommandValidator : AbstractValidator<AssignMasterWaybillFromDesktopCommand>
{
    public AssignMasterWaybillFromDesktopCommandValidator()
    {
        RuleFor(x => x.No)
            .GreaterThan(0).WithMessage("شماره فرم مانیفست باید بزرگتر از صفر باشد");

        RuleFor(x => x.CompanySenderCaptain360Code)
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت فرستنده الزامی است")
            .MinimumLength(1).WithMessage("کد کاپیتان 360 شرکت فرستنده نمی‌تواند کمتر از 1 کاراکتر باشد");

        RuleFor(x => x.MasterWaybillNo)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.MasterWaybillNo))
            .WithMessage("شماره بارنامه نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.MasterWaybillWeight)
            .GreaterThan(0).WithMessage("وزن خالص باید بزرگتر از صفر باشد");

        RuleFor(x => x.MasterWaybillAirline)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.MasterWaybillAirline))
            .WithMessage("نام ایرلاین نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.MasterWaybillFlightNo)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.MasterWaybillFlightNo))
            .WithMessage("شماره پرواز بار نمی‌تواند بیشتر از 50 کاراکتر باشد");
    }
}