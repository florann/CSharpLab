namespace WebApp.Domain.Services.Interfaces
{
    public interface IFtpService
    {
        public bool Connect();
        public bool Disconnect();
        public bool PullFiles();
        public bool ListFiles();
        public bool ChangeDir();
        public bool ListDir();
    }
}
