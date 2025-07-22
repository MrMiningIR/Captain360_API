using System.Text.Json.Serialization;

namespace Capitan360.Domain.Constants;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortDirection
{
    Ascending,
    Descending
}
