using Microsoft.Extensions.Options;
using WebApp.Domain.Services.Interfaces;
using WebApp.Worker.Configurations;

namespace WebApp.Worker.HostedService
{
    public class FtpIntegratorHostedService(
        IOptions<FtpIntegratorHostedServiceConfiguration> optionConfiguration,
        ILogger<FtpIntegratorHostedService> logger,
        IIntegratorService ftpIntegratorService) : BaseHostedService(optionConfiguration, logger)
    {
        protected override async Task RunAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Start FtpIntegrator job");

            await ftpIntegratorService.ProcessFileIntegrationAsync();

            logger.LogInformation("End FtpIntegrator job");
        }
    }
}
