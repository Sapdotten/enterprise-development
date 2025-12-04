using System.Reflection.Metadata;
using AutoMapper;
using Library.Application;
using Library.Application.Contracts.Interfaces;
using Library.Application.Services;
using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Library.Infrastructure.Kafka;
using Library.Infrastructure.Kafka.Deserializers;
using Library.Infrastructure.Postgres;
using Library.Infrastructure.Postgres.Repositories;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

var mapperConfig = new MapperConfiguration(
    config => config.AddProfile(new MappingProfile()),
    LoggerFactory.Create(builder => builder.AddConsole()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<AppDbContext>(connectionName: "DefaultConnection");

builder.Services.AddHostedService<KafkaConsumer>();

builder.Services.AddSingleton<KeyDeserializer>();
builder.Services.AddSingleton<ValueDeserializer>();
builder.Services.AddHostedService<KafkaConsumer>();
builder.Services.AddScoped<IRepository<Book, int>, BookRepository>();
builder.Services.AddScoped<IRepository<Reader, int>, ReaderRepository>();
builder.Services.AddScoped<IRepository<LoanRecord, int>, LoanRecordRepository>();

builder.Services.AddScoped<ILoanRecordService, LoanRecordService>();
builder.Services.Configure<KafkaConsumerOptions>(builder.Configuration.GetSection("Kafka"));


var host = builder.Build();
host.Run();