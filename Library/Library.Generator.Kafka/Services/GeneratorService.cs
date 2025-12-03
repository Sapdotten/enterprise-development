using Confluent.Kafka;
using Library.Application.Contracts.Dtos;
using Polly;
using Polly.Retry;

namespace Library.Generator.Kafka.Services;

/// <summary>
/// Service for sending book loan record batches to Kafka.
/// Uses retry policy to handle transient failures during message production.
/// </summary>
public class GeneratorService : IProducerService
{
    private readonly string _topicName;
    private readonly ILogger _logger;
    private readonly IProducer<Guid, IList<LoanRecordCreateDto>> _producer;
    private readonly AsyncRetryPolicy _retryPolicy;

    /// <summary>
    /// Initializes a new instance of the GeneratorService class.
    /// Configures Kafka topic, logger, producer client, and retry policy.
    /// </summary>
    /// <param name="configuration">Application configuration containing Kafka settings.</param>
    /// <param name="producer">Pre-configured Kafka producer instance.</param>
    /// <param name="logger">Logger instance for diagnostics.</param>
    /// <exception cref="KeyNotFoundException">Thrown when Kafka topic name is missing in configuration.</exception>
    public GeneratorService(IConfiguration configuration,
                           IProducer<Guid, IList<LoanRecordCreateDto>> producer,
                           ILogger<GeneratorService> logger)
    {
        _topicName = configuration.GetSection("Kafka")["Topic"]
                     ?? throw new KeyNotFoundException("TopicName section of Kafka is missing");

        _producer = producer;
        _logger = logger;

        _retryPolicy = Policy
            .Handle<ProduceException<Guid, IList<LoanRecordCreateDto>>>()
            .Or<KafkaException>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(2),
                onRetry: (ex, delay, retry, ctx) =>
                {
                    _logger.LogWarning(ex, "Error: send to Kafka fail. Attempt to {Retry} after {Delay}", retry, delay.TotalSeconds);
                });
    }

    /// <summary>
    /// Sends a batch of loan record DTOs to the configured Kafka topic.
    /// Applies retry logic in case of transient errors.
    /// </summary>
    /// <param name="batch">List of LoanRecordCreateDto to send.</param>
    public async Task SendAsync(IList<LoanRecordCreateDto> batch)
    {
        try
        {
            _logger.LogInformation("Sending a batch of {count} checkouts to {topic}", batch.Count, _topicName);
            await _retryPolicy.ExecuteAsync(async () =>
            {
                var message = new Message<Guid, IList<LoanRecordCreateDto>>
                {
                    Key = Guid.NewGuid(),
                    Value = batch
                };

                await _producer.ProduceAsync(_topicName, message);

                _logger.LogInformation("Batch of {count} checkouts sent successfully", batch.Count);
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred during sending a batch of {count} checkouts to {topic}", batch.Count, _topicName);
        }
    }
}