namespace Library.Api.Kafka;

/// <summary>
/// Options for Kafka consumer configuration.
/// </summary>
public class KafkaConsumerOptions
{
    /// <summary>
    /// Kafka topic name to consume from.
    /// </summary>
    public string? Topic { get; set; }

    /// <summary>
    /// Consumer group ID.
    /// </summary>
    public string? GroupId { get; set; }
}
