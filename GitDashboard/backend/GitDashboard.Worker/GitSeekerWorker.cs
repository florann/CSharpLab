using CodeEditor.Domain.Helpers;
using CodeEditor.Worker.Configuration;
using CodeEditor.Worker.Helpers;
using CodeEditor.Worker.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace CodeEditor.Worker;

public class GitSeekerWorker(
    ILogger<GitSeekerWorker> logger,
    IOptions<GitSeekerConfiguration> configuration,
    IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await using var scope = serviceScopeFactory.CreateAsyncScope();
                var gitSeekerService = scope.ServiceProvider.GetRequiredService<IGitSeekerService>();

                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                if (!await gitSeekerService.HealthCheck())
                    return;

                var gitRepositories = await gitSeekerService.GetGitRepoToPull();

                foreach (var gitRepo in gitRepositories)
                {
                    await gitSeekerService.SeekUpate(gitRepo);
                    logger.LogInformation("Success seeking repository - {GitRepo}", gitRepo.ToString());
                }
            }
            catch (EnhancedException enhancedEx)
            {
                logger.LogError("Error seeking repository - Exception message : {Message} - Function name : {FunctionName} - Params : {Params}",
                    enhancedEx.Message,
                    enhancedEx.FunctionName,
                    enhancedEx.Arguments);
            }
            catch (Exception ex)
            {
                logger.LogError("Error - Exception message : {Message}", ex.Message);
            }


            await Task.Delay(CronHelper.CronToMilliseconds(configuration.Value.Schedule), stoppingToken);
        }
    }
}
