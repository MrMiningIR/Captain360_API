using Microsoft.AspNetCore.Identity;

namespace Capitan360.Application.Features.Identities.Identities.CustomIdentityErrorDescriber;

public class CustomIdentityErrorDescriberMessage : IdentityErrorDescriber
{
    // پیام برای username تکراری
    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateUserName),
            Description = $"نام کاربری '{userName}' قبلاً ثبت شده است. لطفاً نام کاربری دیگری انتخاب کنید."
        };
    }

    // پیام برای ایمیل تکراری
    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateEmail),
            Description = $"ایمیل '{email}' قبلاً استفاده شده است. لطفاً ایمیل دیگری وارد کنید."
        };
    }

    // پیام برای رمز عبور نامعتبر
    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError
        {
            Code = nameof(PasswordTooShort),
            Description = $"رمز عبور باید حداقل {length} کاراکتر باشد."
        };
    }

    // پیام برای رمز عبور که نیاز به عدد دارد
    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresDigit),
            Description = "رمز عبور باید حداقل شامل یک عدد باشد."
        };
    }

    // پیام برای لاگین ناموفق
    public override IdentityError InvalidUserName(string userName)
    {
        return new IdentityError
        {
            Code = nameof(InvalidUserName),
            Description = $"نام کاربری '{userName}' نامعتبر است. لطفاً بررسی کنید."
        };
    }

    // می‌تونی هر متد دیگه‌ای رو هم override کنی
}