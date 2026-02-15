using CodeEditor.Worker;

var builder = Host.CreateApplicationBuilder(args);



builder.Services.AddHostedService<GitSeekerWorker>();

var host = builder.Build();
host.Run();
