namespace CodeEditor.Domain.Requests.AuthRequests
{
    public class CreateAccountRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
