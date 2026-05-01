using RabbitMQ.Client;

namespace Microservice.Alpha.Worker
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

                var message = System.Text.Encoding.UTF8.GetBytes("JEMANGECACA");

                await channel.BasicPublishAsync("", "alpha", message, cancellationToken: stoppingToken); 

                await Task.Delay(1000, stoppingToken);
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
