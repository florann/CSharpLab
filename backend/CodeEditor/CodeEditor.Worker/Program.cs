using CodeEditor.Worker;
using CodeEditor.Worker.Configuration;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.GetSection("GitSeekerConfiguration").Get<GitSeekerConfiguration>();

builder.Services.AddHostedService<GitSeekerWorker>();

var host = builder.Build();
host.Run();
