using FluentValidation;

namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Commands.Update;

public class UpdateCompanyInsuranceCommandValidator : AbstractValidator<UpdateCompanyInsuranceCommand>
{
    public UpdateCompanyInsuranceCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه شرکت بیمه باید بزرگتر از صفر باشد");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("کد شرکت الزامی است")
            .MaximumLength(10).WithMessage("کد شرکت نمی‌تواند بیشتر از 10 کاراکتر باشد")
            .MinimumLength(1).WithMessage("کد شرکت نمی‌تواند کمتر از 1 کاراکتر باشد");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام شرکت الزامی است")
            .MaximumLength(30).WithMessage("نام شرکت نمی‌تواند بیشتر از 30 کاراکتر باشد")
            .MinimumLength(1).WithMessage("نام شرکت نمی‌تواند کمتر از 1 کاراکتر باشد");

        RuleFor(x => x.CaptainCargoCode)
            .NotEmpty().WithMessage("کد کاپیتان کارگو شرکت الزامی است")
            .MaximumLength(10).WithMessage("کد کاپیتان کارگو نمی‌تواند بیشتر از 10 کاراکتر باشد")
            .MinimumLength(1).WithMessage("کد کاپیتان کارگو نمی‌تواند کمتر از 1 کاراکتر باشد");

        RuleFor(x => x.Tax)
            .InclusiveBetween(0m, 100m).WithMessage("مالیات باید بین 0 تا 100 باشد.")
            .PrecisionScale(5, 2, true).WithMessage("مالیات باید حداکثر 2 رقم اعشار و مجموعاً حداکثر 5 رقم داشته باشد.");

        RuleFor(x => x.Scale)
            .NotEmpty().WithMessage("مقیاس بیمه الزامی است")
            .GreaterThan(0).WithMessage("مقیاس باید بزرگتر از صفر باشد");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("توضیحات الزامی است.")
            .MaximumLength(500).WithMessage("توضیحات نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}