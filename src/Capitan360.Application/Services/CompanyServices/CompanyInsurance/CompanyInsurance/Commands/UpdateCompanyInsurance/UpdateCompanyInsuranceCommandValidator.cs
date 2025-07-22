using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsurance.Commands.UpdateCompanyInsurance;

public class UpdateCompanyInsuranceCommandValidator : AbstractValidator<UpdateCompanyInsuranceCommand>
{
    public UpdateCompanyInsuranceCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("کد بیمه الزامی است")
            .MaximumLength(50).WithMessage("کد بیمه نمی‌تواند بیشتر از 50 کاراکتر باشد");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام بیمه الزامی است")
            .MaximumLength(500).WithMessage("نام بیمه نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.Tax)
            .GreaterThanOrEqualTo(0).WithMessage("مالیات باید بزرگ‌تر یا برابر با صفر باشد");

        RuleFor(x => x.Scale)
            .GreaterThanOrEqualTo(0).WithMessage("مقیاس باید بزرگ‌تر یا برابر با صفر باشد");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("توضیحات الزامی است")
            .MaximumLength(500).WithMessage("توضیحات نمی‌تواند بیشتر از 500 کاراکتر باشد");

        RuleFor(x => x.CompanyTypeId)
            .GreaterThan(0).WithMessage("شناسه نوع شرکت الزامی است");

        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت الزامی است");
    }
}