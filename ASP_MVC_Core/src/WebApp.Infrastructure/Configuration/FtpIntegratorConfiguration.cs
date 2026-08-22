namespace WebApp.Infrastructure.Configuration
{
    public class FtpIntegratorConfiguration : FtpConfiguration
    {
        public required string IntegratorFolder { get; set; }
    }
}
