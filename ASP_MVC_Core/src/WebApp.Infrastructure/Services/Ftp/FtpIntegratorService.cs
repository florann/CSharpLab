using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using WebApp.Domain.Entities;
using WebApp.Infrastructure.Configuration;

namespace WebApp.Infrastructure.Services.Ftp
{
    public class FtpIntegratorService(IOptions<FtpIntegratorConfiguration> options, ILogger<FtpIntegratorService> logger) : FtpService<Sensor>(options, logger)
    {
        protected override async Task<Sensor?> ProcessFileData(string fullName, Stream stream, CancellationToken cancellationToken)
        {
            Sensor? sensor = await JsonSerializer.DeserializeAsync<Sensor>(stream, cancellationToken: cancellationToken);
            if (sensor == null)
            {
                logger.LogWarning("Unable to convert stream to Sensor object for file - {Name}", fullName);
                return default;
            }

            throw new NotImplementedException();
        }
    }
}
