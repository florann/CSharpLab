using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApp.Infrastructure.Configuration;

namespace WebApp.Infrastructure.Services.Ftp
{
    public class FtpIntegratorService(IOptions<FtpIntegratorConfiguration> options, ILogger<FtpService> logger) : FtpService(options, logger)
    {
        protected override Task<T> ProcessFileData<T>(Stream stream, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
