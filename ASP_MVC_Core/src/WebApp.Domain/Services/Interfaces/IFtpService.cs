using FluentFTP;

namespace WebApp.Domain.Services.Interfaces
{
    public interface IFtpService
    {
        Task<bool> ConnectAsync(CancellationToken cancellationToken = default);

        Task<bool> DisconnectAsync(CancellationToken cancellationToken = default);

        Task<bool> ChangeDirAsync(string? remotePath = null, CancellationToken cancellationToken = default);

        Task<List<FtpListItem>> ListDirAsync(string? remotePath = null, CancellationToken cancellationToken = default);

        Task<List<string>> ListFilesAsync(string? remotePath = null, CancellationToken cancellationToken = default);

        Task<List<string>> PullFilesAsync(string? remoteDir = null, string? localDir = null, CancellationToken cancellationToken = default);
    }
}
