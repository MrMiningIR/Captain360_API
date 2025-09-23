using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestFormPeriods.Commands.Update;

public class UpdateManifestFormPeriodCommandValidator : AbstractValidator<UpdateManifestFormPeriodCommand>
{
    public UpdateManifestFormPeriodCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه مخزن باید بزرگتر از صفر باشد");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("کد مخزن الزامی است")
            .MinimumLength(1).WithMessage("کد مخزن نمی‌تواند کمتر از 1 کاراکتر باشد")
            .MaximumLength(5).WithMessage("کد مخزن نمی‌تواند بیشتر از 5 کاراکتر باشد");

        RuleFor(x => x.StartNumber)
            .GreaterThan(0).WithMessage("شماره شروع مخزن باید بزرگتر از صفر باشد");

        RuleFor(x => x.EndNumber)
            .GreaterThan(0).WithMessage("شماره پایان مخزن باید بزرگتر از صفر باشد");

        RuleFor(x => x.EndNumber)
            .GreaterThanOrEqualTo(x => x.StartNumber)
            .WithMessage("شماره پایان مخزن باید بزرگتر یا مساوی شماره شروع مخزن باشد");

        RuleFor(x => x.EndNumber - x.StartNumber + 1)
            .LessThanOrEqualTo(2000)
            .WithMessage("اختلاف شماره پایان و شروع مخزن نباید بیشتر از 2000 باشد");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithMessage("توضیحات مخزن نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}