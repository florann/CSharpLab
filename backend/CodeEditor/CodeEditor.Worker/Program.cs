using CodeEditor.Domain.Mapper;
using CodeEditor.Worker;
using CodeEditor.Worker.Configuration;
using CodeEditor.Worker.Extensions;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.AddConfiguration();

builder.Services.AddHostedService<GitSeekerWorker>();

builder.Services.AddDependencies();

builder.Services.AddAutoMapper(typeof(ProfileConfiguration).Assembly);

var host = builder.Build();
host.Run();
