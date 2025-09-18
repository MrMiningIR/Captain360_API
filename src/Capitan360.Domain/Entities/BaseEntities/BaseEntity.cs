using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Capitan360.Domain.Entities.BaseEntities;

public abstract class BaseEntity
{
    public int Id { get; set; }

    [Timestamp]
    [JsonIgnore]
    public byte[] ConcurrencyToken { get; set; } = Array.Empty<byte>();
}