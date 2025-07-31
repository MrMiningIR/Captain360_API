using System.Text.Json.Serialization;

namespace Capitan360.Domain.Abstractions
{
    public abstract class Entity
    {
        protected Entity(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        [JsonIgnore]
        public byte[] ConcurrencyToken { get; set; }

        protected Entity()
        {
        }
    }
}