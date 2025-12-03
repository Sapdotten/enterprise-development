using Library.Application.Contracts.Dtos;
using Confluent.Kafka;
using System.Text.Json;

namespace Library.Generator.Kafka.Serializers;

public class ValueSerializer : ISerializer<IList<LoanRecordCreateDto>>
{
    public byte[] Serialize(IList<LoanRecordCreateDto> list, SerializationContext context) => JsonSerializer.SerializeToUtf8Bytes(list);
}