using CodeEditor.Worker.Configuration;
using CodeEditor.Worker.Helpers;
using Microsoft.Extensions.Options;

namespace CodeEditor.Worker;

public class GitSeekerWorker(
    ILogger<GitSeekerWorker> logger,
    IOptions<GitSeekerConfiguration> configuration) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(CronHelper.CronToMilliseconds(configuration.Value.Schedule), stoppingToken);
        }
    }
}
