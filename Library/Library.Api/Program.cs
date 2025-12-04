using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Library.Application;
using Library.Application.Services;
using Library.Application.Contracts.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Library.Application.Contracts.Dtos;
using Library.Api;
using Library.Api.Kafka;
using Library.Infrastructure.Postgres;
using Library.Infrastructure.Postgres.Repositories;
using Library.Api.Kafka.Deserializers;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();


var mapperConfig = new MapperConfiguration(
    config => config.AddProfile(new MappingProfile()),
    LoggerFactory.Create(builder => builder.AddConsole())
);
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<LoggingActionFilter>();

builder.AddNpgsqlDbContext<AppDbContext>(connectionName: "DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IRepository<Reader, int>, ReaderRepository>();
builder.Services.AddScoped<IRepository<Book, int>, BookRepository>();
builder.Services.AddScoped<IRepository<LoanRecord, int>, LoanRecordRepository>();

builder.Services.AddScoped<IApplicationService<BookGetDto, BookCreateDto, int>, BookService>();
builder.Services.AddScoped<IApplicationService<ReaderGetDto, ReaderCreateDto, int>, ReaderService>();
builder.Services.AddScoped<IApplicationService<LoanRecordGetDto, LoanRecordCreateDto, int>, LoanRecordService>();
builder.Services.AddScoped<ILibraryAnalyticsService, LibraryAnalyticsService>();

builder.Services.AddSwaggerGen(c =>
{
    var apiXmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);
    c.IncludeXmlComments(apiXmlPath);

    var contractsXmlFile = "Library.Application.Contracts.xml";
    var contractsXmlPath = Path.Combine(AppContext.BaseDirectory, contractsXmlFile);
    c.IncludeXmlComments(contractsXmlPath);
});

builder.Services.AddSingleton<KeyDeserializer>();
builder.Services.AddSingleton<ValueDeserializer>();
builder.Services.Configure<KafkaConsumerOptions>(builder.Configuration.GetSection("Kafka"));
builder.Services.AddScoped<ILoanRecordService, LoanRecordService>();

builder.Services.AddHostedService<KafkaConsumer>();

var app = builder.Build();

app.MapDefaultEndpoints();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
    DataInitializer.Seed(dbContext);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();