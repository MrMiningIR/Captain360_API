using FluentValidation;
using System.Text.RegularExpressions;

namespace Capitan360.Application.Extensions;

public static class PersianTimeValidatorExtensions
{
    public static IRuleBuilderOptions<T, string?> IsValidTime<T>(
        this IRuleBuilder<T, string?> ruleBuilder,
        string fieldName)
    {
        return ruleBuilder
            .Matches(@"^\d{2}:\d{2}$")
                .WithMessage($"{fieldName} باید به صورت HH:MM باشد")
            .Must(BeValidTime)
                .WithMessage($"{fieldName} معتبر نیست");
    }

    private static bool BeValidTime(string? time)
    {
        if (string.IsNullOrWhiteSpace(time))
            return false;

        try
        {
            if (!Regex.IsMatch(time, @"^\d{2}:\d{2}$"))
                return false;

            var parts = time.Split(':');
            int hour = int.Parse(parts[0]);
            int minute = int.Parse(parts[1]);

            return hour >= 0 && hour <= 23 && minute >= 0 && minute <= 59;
        }
        catch
        {
            return false;
        }
    }
}
