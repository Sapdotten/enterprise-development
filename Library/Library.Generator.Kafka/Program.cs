using Library.Application.Contracts.Dtos;
using Library.Generator.Kafka;
using Library.Generator.Kafka.Serializers;
using Library.Generator.Kafka.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.AddKafkaProducer<Guid, IList<LoanRecordCreateDto>>("library-kafka",
    configureBuilder: kafkaBuilder =>
    {
        kafkaBuilder.SetKeySerializer(new KeySerializer());
        kafkaBuilder.SetValueSerializer(new ValueSerializer());

    });

builder.Services.AddSingleton<IProducerService, GeneratorService>();
builder.Services.AddHostedService<KafkaProducerService>();
builder.Services.Configure<GeneratorOptions>(builder.Configuration.GetSection("Generator"));

var host = builder.Build();
host.Run();