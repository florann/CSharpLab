using Cronos;
using Microsoft.Extensions.Options;
using WebApp.Worker.Configurations;

namespace WebApp.Worker.HostedService
{
    public abstract class BaseHostedService(
        IOptions<BaseConfiguration> optionConfiguration,
        ILogger<BaseHostedService> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if(string.IsNullOrEmpty(optionConfiguration.Value.CronTabExpression))
            {
                throw new ApplicationException("CronTabExpression must be set for worker");
            }

            var cronExpression = CronExpression.Parse(optionConfiguration.Value.CronTabExpression) ?? throw new ApplicationException("CronExpression is null");
            logger.LogInformation("Hosted service {Type} is configure to run {CronTabExpression}", GetType().Name, optionConfiguration.Value.CronTabExpression);

            while (!stoppingToken.IsCancellationRequested)
            {
                var next = cronExpression.GetNextOccurrence(DateTime.UtcNow, TimeZoneInfo.Local) ?? throw new ApplicationException("Impossible to calculate next occurence");

                var delay = next - DateTime.UtcNow;
                if(delay > TimeSpan.Zero)
                {
                    await Task.Delay(delay, stoppingToken);
                }

                logger.LogInformation("Running job at {Time}", DateTime.UtcNow);
                await RunAsync(stoppingToken);    
            }
        }

        protected abstract Task RunAsync(CancellationToken stoppingToken);
    }
}
