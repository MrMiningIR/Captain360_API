using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Capitan360.Application.Utils;

public static class Tools
{
    /// <summary>
    /// تبدیل تاریخ میلادی به شمسی
    /// </summary>
    /// <param name="dateTime">تاریخ میلادی</param>
    /// <returns>تاریخ شمسی</returns>
    public static string ConvertGregorianToPersian(DateTime dateTime)
    {
        PersianCalendar persianCalendar = new PersianCalendar();
        return persianCalendar.GetYear(dateTime).ToString() + "/" + persianCalendar.GetMonth(dateTime).ToString("0#") + "/" + persianCalendar.GetDayOfMonth(dateTime).ToString("0#");
    }

    /// <summary>
    /// تبدیل تاریخ میلادی به شمسی
    /// </summary>
    /// <returns>تاریخ شمسی</returns>
    public static string GetTodayInPersianDate()
    {
        DateTime dateTime = DateTime.Now;
        PersianCalendar persianCalendar = new PersianCalendar();
        return persianCalendar.GetYear(dateTime).ToString() + "/" + persianCalendar.GetMonth(dateTime).ToString("0#") + "/" + persianCalendar.GetDayOfMonth(dateTime).ToString("0#");
    }

    /// <summary>
    /// تبدیل تاریخ میلادی به شمسی
    /// </summary>
    /// <param name="date">تاریخ میلادی به رشته</param>
    /// <returns>تاریخ شمسی</returns>
    public static DateTime ConvertPersianToGregorian(string date)
    {
        PersianCalendar persianCalendar = new PersianCalendar();
        if (date.Split('/').Count() != 3 || !int.TryParse(date.Split('/')[0], out _) || !int.TryParse(date.Split('/')[1], out _) || !int.TryParse(date.Split('/')[2], out _))
            return DateTime.Now;

        return new DateTime(int.Parse(date.Split('/')[0]), int.Parse(date.Split('/')[1]), int.Parse(date.Split('/')[2]), persianCalendar);
    }

    /// <summary>
    /// تبدیل زمان به حالت 00:00
    /// </summary>
    /// <returns>زمان</returns>
    public static string GetTime()
    {
        DateTime dateTime = DateTime.Now;
        return dateTime.Hour.ToString("0#") + ":" + (dateTime.Minute).ToString("0#");
    }

    /// <summary>
    /// تبدیل زمان به حالت 00:00
    /// </summary>
    /// <returns>زمان</returns>
    public static string GetTime(DateTime dateTime)
    {
        return dateTime.Hour.ToString("0#") + ":" + (dateTime.Minute).ToString("0#");
    }













    public static string GenerateRandomSessionId() => Guid.NewGuid().ToString();


    public static string GetEnumDisplayName(Enum enumType)
    {
        var displayAttribute = enumType.GetType()
                                    .GetField(enumType.ToString())
                                    .GetCustomAttributes(typeof(DisplayAttribute), false)
                                    .FirstOrDefault() as DisplayAttribute;

        return displayAttribute?.Name ?? enumType.ToString();
    }

    public static Guid GenerateDeterministicGuid(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Input cannot be null or empty.", nameof(input));
        }


        using var md5 = MD5.Create();
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);


        return new Guid(hashBytes);
    }
}
