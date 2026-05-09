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
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received: {message}");

                await Task.Delay(2000);

                await channel.BasicAckAsync(ea.DeliveryTag, false);                
            };


            await channel.BasicQosAsync(0, 1, false, stoppingToken);

            await channel.BasicConsumeAsync("alpha", false, consumer, cancellationToken: stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

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