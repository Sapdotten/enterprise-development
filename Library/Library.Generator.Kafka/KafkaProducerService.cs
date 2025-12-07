using Library.Generator.Kafka.Services;
using Microsoft.Extensions.Options;

namespace Library.Generator.Kafka;


/// <summary>
/// Background service for generating and sending a specified number of contracts at defined intervals.
/// </summary>
public class KafkaProducerService : BackgroundService
{
    private readonly GeneratorOptions _options;
    private readonly IProducerService _producer;
    private readonly ILogger<KafkaProducerService> _logger;

    /// <summary>
    /// Initializes a new instance of the KafkaProducerService class.
    /// Loads generation parameters via IOptions and validates them.
    /// </summary>
    /// <param name="options">Generator configuration wrapped in IOptions.</param>
    /// <param name="producer">Kafka producer service.</param>
    /// <param name="logger">Logger instance.</param>
    /// <exception cref="ArgumentException">Thrown when any generator parameter is invalid.</exception>
    public KafkaProducerService(IOptions<GeneratorOptions> options, IProducerService producer, ILogger<KafkaProducerService> logger)
    {
        _options = options.Value;

        if (_options.BatchSize <= 0)
            throw new ArgumentException($"Invalid argument value for BatchSize: {_options.BatchSize}");
        if (_options.PayloadLimit <= 0)
            throw new ArgumentException($"Invalid argument value for PayloadLimit: {_options.PayloadLimit}");
        if (_options.WaitTime <= 0)
            throw new ArgumentException($"Invalid argument value for WaitTime: {_options.WaitTime}");

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
        _logger.LogInformation(
            "Starting to send {total} messages with {time}s interval with {batch} messages in batch",
            _options.PayloadLimit, _options.WaitTime, _options.BatchSize);

        var counter = 0;
        while (counter < _options.PayloadLimit && !stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _producer.SendAsync(LoanRecordGenerator.GenerateLinks(_options.BatchSize));
                counter += _options.BatchSize;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Send batch with error. Retry");
            }
            await Task.Delay(TimeSpan.FromSeconds(_options.WaitTime), stoppingToken);
        }

        _logger.LogInformation(
            "Finished sending {total} messages with {time}s interval with {batch} messages in batch",
            _options.PayloadLimit, _options.WaitTime, _options.BatchSize);
    }
}