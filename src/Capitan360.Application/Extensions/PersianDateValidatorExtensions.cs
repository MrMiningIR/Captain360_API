using FluentValidation;
using System.Globalization;

namespace Capitan360.Application.Extensions;

public static class PersianDateValidatorExtensions
{
    public static IRuleBuilderOptions<T, string?> IsValidPersianDate<T>(
        this IRuleBuilder<T, string?> ruleBuilder,
        string fieldName)
    {
        return ruleBuilder
            .Matches(@"^\d{4}/\d{2}/\d{2}$")
                .WithMessage($"{fieldName} باید به صورت YYYY/MM/DD باشد")
            .Must(BeValidPersianDate)
                .WithMessage($"{fieldName} معتبر نیست");
    }


    private static bool BeValidPersianDate(string? date)
    {
        if (string.IsNullOrWhiteSpace(date))
            return false;

        try
        {
            var parts = date.Split('/');
            if (parts.Length != 3) return false;

            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);

            var persianCalendar = new PersianCalendar();
            _ = persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
