using Capitan360.Application.Features.Companies.UserCompany.Commands.Create;
using FluentValidation;

namespace Capitan360.Application.Features.Companies.UserCompany.Commands.CreateUserCompany;

public class CreateUserCompanyCommandValidator : AbstractValidator<CreateUserCompanyCommand>
{
    public CreateUserCompanyCommandValidator()
    {
        // 1. NameFamily: نباید خالی باشد، حداقل 5 کاراکتر، حداکثر 100 کاراکتر
        RuleFor(x => x.NameFamily)
            .NotEmpty().WithMessage("نام کامل نمی‌تواند خالی باشد.")
            .MinimumLength(5).WithMessage("نام کامل باید حداقل 5 کاراکتر باشد.")
            .MaximumLength(100).WithMessage("نام کامل باید حداکثر 100 کاراکتر باشد.");

        // 2. PhoneNumber: اجباری، دقیقاً 11 کاراکتر
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("شماره تلفن نمی‌تواند خالی باشد.")
            .Length(11).WithMessage("شماره تلفن باید دقیقاً 11 رقم باشد.");

        // 3. Email: اجباری نیست، اما اگر وجود داشته باشد، باید فرمت ایمیل داشته باشد
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("فرمت ایمیل نامعتبر است.")
            .When(x => !string.IsNullOrEmpty(x.Email));  // فقط اگر ایمیل وارد شده باشد، چک شود

        // 4. Password: اجباری، فقط شامل اعداد، حداقل 6 کاراکتر، حداکثر 10 کاراکتر
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("پسورد نمی‌تواند خالی باشد.")
            .Matches(@"^\d+$").WithMessage("پسورد باید فقط شامل اعداد باشد.")  // فقط اعداد
            .MinimumLength(6).WithMessage("پسورد باید حداقل 6 کاراکتر باشد.")
            .MaximumLength(10).WithMessage("پسورد باید حداکثر 10 کاراکتر باشد.");

        // 5. ConfirmPassword: باید با Password مطابقت داشته باشد
        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("تایید پسورد با پسورد اصلی مطابقت ندارد.")
            .When(x => !string.IsNullOrEmpty(x.ConfirmPassword));  // فقط اگر ConfirmPassword وارد شده باشد، چک شود

        //6.TypeOfFactorInSamanehMoadianId: باید یکی از مقادیر enum باشد (1, 2 یا 3)، و اجباری
        // اگر پراپرتی را به نوع enum تغییر دهید(public TypeOfFactorInSamanehMoadianId TypeOfFactorInSamanehMoadianId { get; set; }):
        RuleFor(x => x.TypeOfFactorInSamanehMoadianId).IsInEnum().WithMessage("نوع فاکتور موادیان باید یکی از مقادیر مجاز باشد.");

        // اگر پراپرتی int است:
        //RuleFor(x => x.TypeOfFactorInSamanehMoadianId)
        //        .NotEmpty().WithMessage("نوع فاکتور موادیان الزامی است.")  // برای int، NotEmpty کار نمی‌کند، پس از GreaterThan استفاده می‌کنیم
        //        .InclusiveBetween(1, 3).WithMessage("نوع فاکتور موادیان باید یکی از مقادیر 1, 2 یا 3 باشد.");

        // 7. CompanyId: نباید منفی باشد، اجباری و بیشتر از 0
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت باید عددی مثبت بزرگتر از 0 باشد.");
    }
}