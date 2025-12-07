namespace Library.Generator.Kafka;

/// <summary>
/// Configuration options for Kafka message generation.
/// </summary>
public class GeneratorOptions
{
    /// <summary>
    /// Number of messages to send per batch.
    /// </summary>
    public int BatchSize { get; set; } = 100;

    /// <summary>
    /// Total number of messages to generate and send.
    /// </summary>
    public int PayloadLimit { get; set; } = 1000;

    /// <summary>
    /// Delay between batches in seconds.
    /// </summary>
    public int WaitTime { get; set; } = 1;
}
