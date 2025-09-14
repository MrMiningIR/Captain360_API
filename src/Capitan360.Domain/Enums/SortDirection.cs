using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Capitan360.Domain.Constants;

namespace Capitan360.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortDirection
{
    [Display(Name = ConstantNames.Ascending)]
    Ascending,
   
    [Display(Name = ConstantNames.Descending)]
    Descending
}
