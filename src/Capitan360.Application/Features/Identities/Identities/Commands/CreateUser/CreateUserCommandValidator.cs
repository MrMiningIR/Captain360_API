using Capitan360.Domain.Enums;
using FluentValidation;

namespace Capitan360.Application.Features.Identities.Identities.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{


    public CreateUserCommandValidator()
    {
        RuleFor(x => x.NameFamily)
            .NotEmpty()
            .WithMessage("نام و نام خانوادگی الزامی است")
            .MaximumLength(100)
            .WithMessage("نام و نام خانوادگی نباید بیش از ۱۰۰ کاراکتر باشد")
            .MinimumLength(5)
            .WithMessage("نام و نام خانوادگی باید حداقل ۵ کاراکتر باشد");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("شماره تلفن الزامی است")
            .Matches(@"^\d{11}$")
            .WithMessage("شماره تلفن باید ۱۱ رقم باشد");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("رمز عبور الزامی است")
            .MinimumLength(6)
            .WithMessage("رمز عبور باید حداقل ۶ کاراکتر باشد")
            .Matches(@"[A-Za-z0-9]+")
            .WithMessage("رمز عبور باید فقط شامل حروف و اعداد باشد");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("رمز عبور و تکرار آن مطابقت ندارند");

        RuleFor(dto => dto.TypeOfFactorInSamanehMoadianId)
            .Must(moadianType => Enum.IsDefined(typeof(MoadianFactorType), moadianType))
            .When(x => x.TypeOfFactorInSamanehMoadianId != 0)
            .WithMessage($"نوع مودیان انتخاب‌شده معتبر نیست. گزینه‌های مجاز: {string.Join(", ", Enum.GetValues(typeof(MoadianFactorType)))}");


        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("ایمیل باید به فرمت معتبر باشد");

        //RuleFor(x => x.CompanyId)
        //    .GreaterThan(0)
        //    .When(x => x.CompanyId.HasValue); 

    }
}



