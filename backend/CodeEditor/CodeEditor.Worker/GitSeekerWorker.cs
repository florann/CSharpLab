using CodeEditor.Worker.Configuration;
using CodeEditor.Worker.Helpers;
using CodeEditor.Worker.Services;
using CodeEditor.Worker.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace CodeEditor.Worker;

public class GitSeekerWorker(
    ILogger<GitSeekerWorker> logger,
    IOptions<GitSeekerConfiguration> configuration,
    IGitSeekerService gitSeekerService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            if (!await gitSeekerService.HealthCheck())
                return;


            await gitSeekerService.Seek()

            await Task.Delay(CronHelper.CronToMilliseconds(configuration.Value.Schedule), stoppingToken);
        }
    }
}
