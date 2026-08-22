using FluentFTP;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApp.Domain.Services.Interfaces;
using WebApp.Infrastructure.Configuration;

namespace WebApp.Infrastructure.Services.Ftp
{
    public abstract class FtpService<TData> : IFtpService<TData>, IAsyncDisposable
    {
        private readonly FtpConfiguration _config;
        private readonly ILogger<FtpService<TData>> _logger;
        private AsyncFtpClient? _client;

        public FtpService(IOptions<FtpConfiguration> options, ILogger<FtpService<TData>> logger)
        {
            _config = options.Value;
            _logger = logger;
        }

        public async Task<bool> ConnectAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _client = new AsyncFtpClient(_config.Host, _config.Username, _config.Password, _config.Port);
                _client.Config.ConnectTimeout = 15000;
                _client.Config.DataConnectionConnectTimeout = 15000;

                if (_config.UseSsl)
                {
                    _client.Config.EncryptionMode = FtpEncryptionMode.Explicit;
                    _client.Config.ValidateAnyCertificate = true; // tighten this for production use
                }

                await _client.Connect(cancellationToken);
                _logger.LogInformation("Connected to FTP host {Host}:{Port}", _config.Host, _config.Port);
                return _client.IsConnected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to FTP host {Host}:{Port}", _config.Host, _config.Port);
                return false;
            }
        }

        public async Task<bool> DisconnectAsync(CancellationToken cancellationToken = default)
        {
            if (_client is null)
                return true;

            try
            {
                if (_client.IsConnected)
                    await _client.Disconnect(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to disconnect from FTP host {Host}", _config.Host);
                return false;
            }
            finally
            {
                _client.Dispose();
                _client = null;
            }
        }

        public async Task<bool> ChangeDirAsync(string remotePath, CancellationToken cancellationToken = default)
        {
            EnsureConnected();
            var path = remotePath;

            try
            {
                await _client!.SetWorkingDirectory(path, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to change remote directory to {Path}", path);
                return false;
            }
        }

        public async Task<List<FtpListItem>> ListDirAsync(string remotePath, CancellationToken cancellationToken = default)
        {
            EnsureConnected();
            var path = remotePath;

            var items = await _client!.GetListing(path, cancellationToken);
            return [.. items];
        }

        public async Task<List<FtpListItem>> ListFilesAsync(string remotePath, int batch, CancellationToken cancellationToken = default)
        {
            var items = await ListDirAsync(remotePath, cancellationToken);
            return [.. items
                .Where(i => i.Type == FtpObjectType.File)
                .Take(batch)];
        }

        public async Task<TData?> ReadFileAsync(string fullName, CancellationToken cancellationToken = default)
        {
            if (_client is null)
                return default;


            if (!await _client.FileExists(fullName))
            {
                _logger.LogWarning("File does not exist - {Name}", fullName);
                return default;
            }

            using var stream = await _client.OpenRead(fullName, token: cancellationToken);
            if (stream == null)
            {
                _logger.LogWarning("Not bytes read for file - {Name}", fullName);
                return default;
            }

            return await ProcessFileData(fullName, stream, cancellationToken);
        }

        protected abstract Task<TData?> ProcessFileData(string fullName, Stream stream, CancellationToken cancellationToken);

        public async Task<List<string>> PullFilesAsync(string remoteDir, string localDir, CancellationToken cancellationToken = default)
        {
            EnsureConnected();

            var remote = remoteDir;
            var local = localDir;

            if (string.IsNullOrWhiteSpace(local))
                throw new InvalidOperationException("No local directory specified and LocalDownloadDirectory is not configured.");

            Directory.CreateDirectory(local);

            try
            {
                var results = await _client!.DownloadDirectory(
                    local,
                    remote,
                    FtpFolderSyncMode.Update,
                    FtpLocalExists.Overwrite,
                    FtpVerify.None);

                var downloaded = results.Where(r => r.IsSuccess).Select(r => r.LocalPath).ToList();
                _logger.LogInformation("Downloaded {Count} file(s) from {Remote} to {Local}", downloaded.Count, remote, local);
                return downloaded;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to pull files from {Remote} to {Local}", remote, local);
                throw;
            }
        }

        private void EnsureConnected()
        {
            if (_client is null || !_client.IsConnected)
                throw new InvalidOperationException("FTP client is not connected. Call ConnectAsync() first.");
        }

        public async ValueTask DisposeAsync()
        {
            if (_client is not null)
                await DisconnectAsync();
        }
    }
}