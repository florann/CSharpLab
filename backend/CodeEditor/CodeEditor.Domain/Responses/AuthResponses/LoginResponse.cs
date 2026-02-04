namespace CodeEditor.Domain.Responses.AuthResponses
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long UserId { get; set; }
    }
}
