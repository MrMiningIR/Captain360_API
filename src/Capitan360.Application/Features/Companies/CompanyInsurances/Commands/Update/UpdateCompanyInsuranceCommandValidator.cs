using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyInsurances.Commands.Update;

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
            .MaximumLength(10).WithMessage("نام شرکت نمی‌تواند بیشتر از 10 کاراکتر باشد")
            .MinimumLength(1).WithMessage("نام شرکت نمی‌تواند کمتر از 1 کاراکتر باشد");

        RuleFor(x => x.Tax)
            .NotEmpty().WithMessage("مالیات الزامی است")
            .GreaterThanOrEqualTo(0).WithMessage("مالیات باید بزرگتر یا برابر با صفر باشد");

        RuleFor(x => x.Scale)
            .NotEmpty().WithMessage("مقیاس بیمه الزامی است")
            .GreaterThan(0).WithMessage("مقیاس باید بزرگتر از صفر باشد");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("توضیحات نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}