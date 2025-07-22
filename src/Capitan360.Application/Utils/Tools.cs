using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Capitan360.Application.Utils;

public static class Tools
{
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
