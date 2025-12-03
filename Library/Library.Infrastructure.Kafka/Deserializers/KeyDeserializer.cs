using Confluent.Kafka;
using System.Text.Json;

namespace Library.Infrastructure.Kafka.Deserializers;
public class KeyDeserializer : IDeserializer<Guid>
{
    public Guid Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context) =>
       JsonSerializer.Deserialize<Guid>(data);
}