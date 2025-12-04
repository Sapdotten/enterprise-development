using Confluent.Kafka;
using Library.Application.Contracts.Dtos;
using System.Text.Json;

namespace Library.Api.Kafka.Deserializers;
public class ValueDeserializer : IDeserializer<IList<LoanRecordCreateDto>>
{
    public IList<LoanRecordCreateDto> Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull) return [];
        return JsonSerializer.Deserialize<IList<LoanRecordCreateDto>>(data) ?? [];
    }
}