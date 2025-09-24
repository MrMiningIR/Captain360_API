using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Commands.Create;

public class CreateDomesticWaybillPeriodCommandValidator : AbstractValidator<CreateDomesticWaybillPeriodCommand>
{
    public CreateDomesticWaybillPeriodCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید بزرگتر از صفر باشد");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("کد الزامی است")
            .MaximumLength(10).WithMessage("کد نمی‌تواند بیشتر از 10 کاراکتر باشد");

        RuleFor(x => x.StartNumber)
            .GreaterThan(0).WithMessage("شماره شروع باید بزرگتر از صفر باشد");

        RuleFor(x => x.EndNumber)
            .GreaterThan(0).WithMessage("شماره پایان باید بزرگتر از صفر باشد");

        RuleFor(x => x.EndNumber)
            .GreaterThanOrEqualTo(x => x.StartNumber)
            .WithMessage("شماره پایان باید بزرگتر یا مساوی شماره شروع باشد");

        RuleFor(x => x.EndNumber - x.StartNumber + 1)
            .LessThanOrEqualTo(2000)
            .WithMessage("اختلاف شماره پایان و شروع نباید بیشتر از 2000 باشد");

        RuleFor(x => x.Description)
            .NotNull().WithMessage("توضیحات نمی تواند خالی باشد است.")
            .MaximumLength(500).WithMessage("حداکثر طول توضیحات 500 کاراکتر است.");
    }
}