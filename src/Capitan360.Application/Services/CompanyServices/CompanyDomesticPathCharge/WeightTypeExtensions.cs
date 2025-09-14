using System.ComponentModel.DataAnnotations;
using Capitan360.Domain.Enums;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge;

public static class WeightTypeExtensions
{
    public static string GetDisplayName(this WeightType weightType)
    {
        return weightType.GetType()
            .GetMember(weightType.ToString())
            .First()
            .GetCustomAttributes(typeof(DisplayAttribute), false)
            .Cast<DisplayAttribute>()
            .FirstOrDefault()?.Name ?? weightType.ToString();
    }
}