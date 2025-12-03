using Confluent.Kafka;
using System.Text.Json;

namespace Library.Generator.Kafka.Serializers;

public class KeySerializer : ISerializer<Guid>
{
    public byte[] Serialize(Guid key, SerializationContext context) => JsonSerializer.SerializeToUtf8Bytes(key);
}