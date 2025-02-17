var builder = DistributedApplication.CreateBuilder(args);

var ollama = builder.AddOllama("ollama")
    //.WithGPUSupport()
    .WithDataVolume()
    .WithOpenWebUI();

var chat = ollama.AddModel("chat", "llama3.3");

var apiService = builder.AddProject<Projects.LocalModelAspireApp_ApiService>("apiservice")
    .WithReference(chat)
    .WaitFor(chat);

builder.AddProject<Projects.LocalModelAspireApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
