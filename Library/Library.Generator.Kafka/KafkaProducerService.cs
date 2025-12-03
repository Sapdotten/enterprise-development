using Library.Generator.Kafka.Services;

namespace Library.Generator.Kafka;

/// <summary>
/// Background service for generating and sending a specified number of contracts at defined intervals.
/// </summary>
/// <param name="configuration">Configuration provider.</param>
/// <param name="logger">Logger instance.</param>
public class KafkaProducerService : BackgroundService
{
    private readonly int _batchSize;
    private readonly int _payloadLimit;
    private readonly int _waitTime;
    private readonly IProducerService _producer;
    private readonly ILogger<KafkaProducerService> _logger;

    /// <summary>
    /// Initializes a new instance of the KafkaProducerService class.
    /// Loads generation parameters from configuration and validates them.
    /// </summary>
    /// <param name="configuration">IConfiguration used to retrieve generator settings.</param>
    /// <param name="producer">Kafka producer service.</param>
    /// <param name="logger">Logger instance.</param>
    /// <exception cref="ArgumentException">Thrown when any generator parameter in appsettings.json is invalid.</exception>
    public KafkaProducerService(IConfiguration configuration, IProducerService producer, ILogger<KafkaProducerService> logger)
    {
        _batchSize = configuration.GetValue<int>("Generator:BatchSize");
        _payloadLimit = configuration.GetValue<int>("Generator:PayloadLimit");
        _waitTime = configuration.GetValue<int>("Generator:WaitTime");

        if (_batchSize <= 0)
            throw new ArgumentException($"Invalid argument value for BatchSize: {_batchSize}");
        if (_payloadLimit <= 0)
            throw new ArgumentException($"Invalid argument value for PayloadLimit: {_payloadLimit}");
        if (_waitTime <= 0)
            throw new ArgumentException($"Invalid argument value for WaitTime: {_waitTime}");

        _producer = producer;
        _logger = logger;
    }

    /// <summary>
    /// Generates and sends batches of book loan DTOs at configured intervals until the limit is reached.
    /// Runs as a background task and respects cancellation requests.
    /// </summary>
    /// <param name="stoppingToken">Cancellation token.</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting to send {total} messages with {time}s interval with {batch} messages in batch", _payloadLimit, _waitTime, _batchSize);

        var counter = 0;
        while (counter < _payloadLimit && !stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _producer.SendAsync(LoanRecordGenerator.GenerateLinks(_batchSize));
                counter += _batchSize;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Send batch with error. Retry");
            }
            await Task.Delay(_waitTime, stoppingToken);
        }
        _logger.LogInformation("Finished sending {total} messages with {time}s interval with {batch} messages in batch", _payloadLimit, _waitTime, _batchSize);
    }
}