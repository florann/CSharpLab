using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Microservice.Beta.Worker
{
    public class Worker(ILogger<Worker> logger,
        IChannel channel,
        IConnection connection) : BackgroundService
    {
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await channel.QueueDeclareAsync("alpha", true, false, false, cancellationToken: cancellationToken);

            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Received: {message}");
                    return Task.CompletedTask;    
                };

                var result = await channel.BasicConsumeAsync("alpha", true, consumer);

                Console.WriteLine($"Result: {result}");

                await Task.Delay(2000, stoppingToken);
            }
        }

        public override void Dispose()
        {
            channel.Dispose();
            connection.Dispose();

            base.Dispose();
        }
    }
}