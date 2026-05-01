using Microservice.Alpha.Worker;
using RabbitMQ.Client;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var rabbitMqConfiguration = builder.Configuration.GetSection("RabbitMQ");

if (rabbitMqConfiguration == null
    && !string.IsNullOrWhiteSpace(rabbitMqConfiguration!["Host"]!)
    && !string.IsNullOrWhiteSpace(rabbitMqConfiguration!["Port"]!)
    && !string.IsNullOrWhiteSpace(rabbitMqConfiguration!["UserName"]!)
    && !string.IsNullOrWhiteSpace(rabbitMqConfiguration!["Password"]!)
    && !string.IsNullOrWhiteSpace(rabbitMqConfiguration!["VirtualHost"]!)
    )
    throw new ApplicationException("RabbitMQ configuration must be initialized");

builder.Services.AddSingleton(_ =>
{
    var factory = new ConnectionFactory
    {
        HostName = rabbitMqConfiguration!["Host"]!,
        Port = int.Parse(rabbitMqConfiguration!["Port"]!),
        UserName = rabbitMqConfiguration!["Username"]!,
        Password = rabbitMqConfiguration!["Password"]!,
        VirtualHost = rabbitMqConfiguration!["VirtualHost"]!
    };

    return factory.CreateConnectionAsync().GetAwaiter().GetResult();
});

builder.Services.AddSingleton(sp =>
{
    var factory = sp.GetRequiredService<IConnection>();
    return factory.CreateChannelAsync().GetAwaiter().GetResult();
});

var host = builder.Build();
host.Run();
