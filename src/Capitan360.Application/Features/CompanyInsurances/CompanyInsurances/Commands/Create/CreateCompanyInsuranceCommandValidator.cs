using FluentValidation;

namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Create;

public class CreateCompanyInsuranceCommandValidator : AbstractValidator<CreateCompanyInsuranceCommand>
{
    public CreateCompanyInsuranceCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت الزامی است");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("کد الزامی است")
            .MaximumLength(10).WithMessage("کد نمی‌تواند بیشتر از 10 کاراکتر باشد");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام الزامی است")
            .MaximumLength(30).WithMessage("نام نمی‌تواند بیشتر از 30 کاراکتر باشد");

        RuleFor(x => x.CaptainCargoCode)
            .NotEmpty().WithMessage("کد کاپیتان کارگو الزامی است")
            .MaximumLength(10).WithMessage("کد کاپیتان کارگو نمی‌تواند بیشتر از 10 کاراکتر باشد");

        RuleFor(x => x.Tax)
            .InclusiveBetween(0m, 100m).WithMessage("مالیات بیمه باید بین 0 تا 100 باشد.")
            .PrecisionScale(5, 2, true).WithMessage("مالیات بیمه باید حداکثر 2 رقم اعشار و مجموعاً حداکثر 5 رقم داشته باشد.");

        RuleFor(x => x.Scale)
            .NotEmpty().WithMessage("مقیاس بیمه الزامی است")
            .GreaterThan(0).WithMessage("مقیاس باید بزرگتر از صفر باشد");

        RuleFor(x => x.Description)
            .NotNull().WithMessage("توضیحات نمی تواند خالی باشد است.")
            .MaximumLength(500).WithMessage("توضیحات نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}