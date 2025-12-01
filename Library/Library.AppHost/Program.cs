var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres").AddDatabase("librarydb");

var api = builder.AddProject<Projects.Library_Api>("library-api")
    .WithReference(postgres, "DefaultConnection")
    .WaitFor(postgres);

builder.Build().Run();
