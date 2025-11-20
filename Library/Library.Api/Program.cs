using AutoMapper;
using Library.Application;
using Library.Application.Services;
using Library.Application.Contracts.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using Library.Application.Contracts.Dtos;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(
    config => config.AddProfile(new MappingProfile()),
    LoggerFactory.Create(builder => builder.AddConsole())
);
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<LoggingActionFilter>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IRepository<Book, int>, BookRepository>();
builder.Services.AddSingleton<IRepository<Reader, int>, ReaderRepository>();
builder.Services.AddSingleton<IRepository<LoanRecord, int>, LoanRecordRepository>();

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();