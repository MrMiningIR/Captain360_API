using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.Name ?? enumValue.ToString();
    }
}