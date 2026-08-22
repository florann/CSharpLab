using WebApp.Domain.Services.Interfaces;

namespace WebApp.Domain.Services
{
    public class IntegratorService(IFtpService ftpService) : IIntegratorService
    {
        public Task<bool> ProcessFileIntegrationAsync()
        {

            ftpService.ConnectAsync();

            var files = ftpService.ListFilesAsync();

            ftpService.DisconnectAsync();

            // Connect
            // List files
            // Batch integration
            // Disconnect
            throw new NotImplementedException();
        }
    }
}
