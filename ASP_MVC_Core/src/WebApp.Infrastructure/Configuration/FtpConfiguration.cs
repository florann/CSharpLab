namespace WebApp.Infrastructure.Configuration
{
    public class FtpConfiguration
    {
        // --- Connection ---
        public string Host { get; set; } = default!;
        public int Port { get; set; } = 21;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;

        // --- Security ---
        public bool UseSsl { get; set; } = true;
        public FtpSslMode SslMode { get; set; } = FtpSslMode.Explicit;
        public bool ValidateServerCertificate { get; set; } = true;

        // --- Connection behavior ---
        public int ConnectTimeoutSeconds { get; set; } = 30;
        public int ReadWriteTimeoutSeconds { get; set; } = 60;
        public FtpDataConnectionMode DataConnectionType { get; set; } = FtpDataConnectionMode.Passive;
        public int MaxRetryAttempts { get; set; } = 3;
        public int RetryDelaySeconds { get; set; } = 5;

        // --- Paths ---
        public string RemoteDirectory { get; set; } = "/";
        public string LocalWorkingDirectory { get; set; } = default!;
        public string? ArchiveDirectory { get; set; }
        public string? ErrorDirectory { get; set; }
        public string FilePattern { get; set; } = "*.*";

        // --- File handling ---
        public bool DeleteAfterDownload { get; set; } = false;
        public bool OverwriteExisting { get; set; } = false;
    }

    public enum FtpSslMode
    {
        None,
        Explicit,
        Implicit
    }

    public enum FtpDataConnectionMode
    {
        Passive,
        Active
    }
}


