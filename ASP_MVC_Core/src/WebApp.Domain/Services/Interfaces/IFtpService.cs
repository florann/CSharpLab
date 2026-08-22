using FluentFTP;

namespace WebApp.Domain.Services.Interfaces
{
    public interface IFtpService
    {
        Task<bool> ConnectAsync(CancellationToken cancellationToken = default);

        Task<bool> DisconnectAsync(CancellationToken cancellationToken = default);

        Task<bool> ChangeDirAsync(string remotePath, CancellationToken cancellationToken = default);

        Task<List<FtpListItem>> ListDirAsync(string remotePath, CancellationToken cancellationToken = default);

        Task<List<FtpListItem>> ListFilesAsync(string remotePath, int batch, CancellationToken cancellationToken = default);

        Task<List<string>> PullFilesAsync(string remoteDir, string localDir, CancellationToken cancellationToken = default);

        Task<T?> ReadFileAsync<T>(string fullName, CancellationToken cancellationToken = default);
    }
}
