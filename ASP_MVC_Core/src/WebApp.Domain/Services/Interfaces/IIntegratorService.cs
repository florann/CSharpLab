namespace WebApp.Domain.Services.Interfaces
{
    public interface IIntegratorService
    {
        public Task<bool> ProcessFileIntegrationAsync();
    }
}
