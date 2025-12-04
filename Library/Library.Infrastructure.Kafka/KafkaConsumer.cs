using Confluent.Kafka;
using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;
using Library.Infrastructure.Kafka.Deserializers;
using Microsoft.Extensions.Options;

namespace Library.Infrastructure.Kafka;

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

/// <summary>
/// Background service that consumes book loan record batches from a Kafka topic.
/// Uses IConsumer to read messages and delegates processing to ILoanRecordService via dependency injection.
/// </summary>
public class KafkaConsumer : BackgroundService
{
    private readonly IConsumer<Guid, IList<LoanRecordCreateDto>> _consumer;
    private readonly ILogger<KafkaConsumer> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly string _bootstrapServers;

    /// <summary>
    /// Initializes a new instance of the KafkaConsumer class.
    /// Configures Kafka consumer with settings from IOptions and subscribes to the specified topic.
    /// </summary>
    /// <param name="options">Kafka consumer options.</param>
    /// <param name="configuration">Application configuration for connection strings.</param>
    /// <param name="logger">Logger instance for diagnostics.</param>
    /// <param name="keyDeserializer">Custom deserializer for message keys (Guid).</param>
    /// <param name="valueDeserializer">Custom deserializer for message values (List of LoanRecordCreateDto).</param>
    /// <param name="scopeFactory">Factory for creating service scopes to resolve scoped services like ILoanRecordService.</param>
    public KafkaConsumer(
        IOptions<KafkaConsumerOptions> options,
        IConfiguration configuration,
        ILogger<KafkaConsumer> logger,
        KeyDeserializer keyDeserializer,
        ValueDeserializer valueDeserializer,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;

        var kafkaOptions = options.Value;

        if (string.IsNullOrEmpty(kafkaOptions.Topic))
            throw new ArgumentException("Kafka:Topic is required.");
        if (string.IsNullOrEmpty(kafkaOptions.GroupId))
            throw new ArgumentException("Kafka:GroupId is required.");

        _bootstrapServers = configuration.GetConnectionString("library-kafka")
            ?? throw new KeyNotFoundException("ConnectionString 'library-kafka' is missing");

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _bootstrapServers,
            GroupId = kafkaOptions.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true,
            AllowAutoCreateTopics = true
        };

        _consumer = new ConsumerBuilder<Guid, IList<LoanRecordCreateDto>>(consumerConfig)
            .SetKeyDeserializer(keyDeserializer)
            .SetValueDeserializer(valueDeserializer)
            .SetErrorHandler((_, e) => _logger.LogError("Kafka Error: {Reason}", e.Reason))
            .Build();

        _consumer.Subscribe(kafkaOptions.Topic);
        _logger.LogInformation("Subscribed to Kafka topic: {Topic}", kafkaOptions.Topic);
    }

    /// <summary>
    /// Executes the main consumption loop, reading messages from Kafka and processing them.
    /// Runs until cancellation is requested.
    /// </summary>
    /// <param name="stoppingToken">Cancellation token to signal shutdown.</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = _consumer.Consume(stoppingToken);

                if (result == null || result.IsPartitionEOF)
                    continue;

                _logger.LogInformation(
                    "Received message {Key} with {Count} checkouts (Partition: {Partition}, Offset: {Offset})",
                    result.Message.Key,
                    result.Message.Value.Count,
                    result.Partition,
                    result.Offset
                );

                await ProcessMessageAsync(result.Message.Key, result.Message.Value);
            }
            catch (ConsumeException ex)
            {
                _logger.LogError(ex, "Consume error: {Reason}", ex.Error.Reason);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Kafka consumer cancelled");
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in Kafka consumer");
                await Task.Delay(2000, stoppingToken);
            }
        }
        _logger.LogInformation("Kafka consumer stop");
    }

    /// <summary>
    /// Processes a received Kafka message by saving the loan record data through ILoanRecordService.
    /// Uses dependency injection scope to ensure proper lifecycle management of services.
    /// </summary>
    /// <param name="key">Message identifier (Guid).</param>
    /// <param name="checkouts">List of LoanRecordCreateDto to be saved. Can be null or empty.</param>
    private async Task ProcessMessageAsync(Guid key, IList<LoanRecordCreateDto>? checkouts)
    {
        if (checkouts == null || checkouts.Count == 0)
        {
            _logger.LogWarning("Empty checkouts list for message {Key}", key);
            return;
        }
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var checkoutService = scope.ServiceProvider.GetRequiredService<ILoanRecordService>();

            await checkoutService.ReceiveContractAsync(checkouts);

            _logger.LogInformation("Processed message {Key}: {Count} checkouts saved", key, checkouts.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing message {Key}", key);
        }
    }
}