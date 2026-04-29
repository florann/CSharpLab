namespace CodeEditor.Domain.Records
{
    public record JwtSettings
    {
        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public long AccessTokenExpirationInMinutes { get; set; }
        public long RefreshTokenExpirationInMinutes { get; set; }
    }
}
