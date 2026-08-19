namespace WebApp.Domain.Services.Interfaces
{
    public interface IFtpIntegratorService
    {
        public Task<bool> ProcessFileIntegrationAsync();
    }
}
