using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres").AddDatabase("librarydb");

var api = builder.AddProject<Projects.Library_Api>("library-api")
    .WithReference(postgres, "DefaultConnection")
    .WaitFor(postgres);
var kafka = builder.AddKafka("library-kafka")
    .WithKafkaUI();

var kafkaSettings = builder.Configuration.GetSection("Kafka");
var bootstrapServers = builder.Configuration.GetConnectionString("library-kafka");
var groupId = kafkaSettings["GroupId"];
var topic = kafkaSettings["Topic"];

var generatorSettings = builder.Configuration.GetSection("Generator");
var batchSize = generatorSettings.GetValue("BatchSize", 100);
var payloadLimit = generatorSettings.GetValue("PayloadLimit", 1000);
var waitTime = generatorSettings.GetValue("WaitTime", 5);

var producer = builder.AddProject<Projects.Library_Generator_Kafka>("generator")
    .WithReference(kafka)
    .WaitFor(kafka)
    .WaitFor(postgres)
    .WithEnvironment("Kafka:Topic", topic)
    .WithEnvironment("Generator:BatchSize", batchSize.ToString())
    .WithEnvironment("Generator:PayloadLimit", payloadLimit.ToString())
    .WithEnvironment("Generator:WaitTime", waitTime.ToString());

builder.AddProject<Projects.Library_Infrastructure_Kafka>("consumer")
    .WithReference(kafka)
    .WithReference(postgres, "DefaultConnection")
    .WaitFor(kafka)
    .WaitFor(producer)
    .WithEnvironment("Kafka:GroupId", groupId)
    .WithEnvironment("Kafka:BootstrapServers", bootstrapServers)
    .WithEnvironment("Kafka:Topic", topic);

//builder.AddProject<Projects.Library_Generator_Kafka>("library-generator-kafka");

//builder.AddProject<Projects.Library_Infrastructure_Kafka>("library-infrastructure-kafka");

builder.Build().Run();
