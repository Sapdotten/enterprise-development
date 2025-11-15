using AutoMapper;
using Library.Application;
using Library.Application.Services;
using Library.Application.Contracts.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using Library.Application.Contracts.DTOs;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(
    config => config.AddProfile(new MappingProfile()),
    LoggerFactory.Create(builder => builder.AddConsole())
);
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IRepository<Book, int>, BookRepository>();
builder.Services.AddSingleton<IRepository<Reader, int>, ReaderRepository>();
builder.Services.AddSingleton<IRepository<LoanRecord, int>, LoanRecordRepository>();

builder.Services.AddScoped<IApplicationService<BookGetDTO, BookCreateDTO, int>, BookService>();
builder.Services.AddScoped<IApplicationService<ReaderGetDTO, ReaderCreateDTO, int>, ReaderService>();
builder.Services.AddScoped<IApplicationService<LoanRecordGetDTO, LoanRecordCreateDTO, int>, LoanRecordService>();
builder.Services.AddScoped<ILibraryAnalyticsService, LibraryAnalyticsService>();

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
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