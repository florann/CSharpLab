using Microsoft.AspNetCore.SignalR;

namespace CodeEditor.Api.Hubs
{
    public class ChatHub : Hub
    {
        private ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        public async Task SendMessage(string user, string message)
        {
            try
            {
                _logger.LogInformation("From {User} - Message {Message}", user, message);
                await Clients.All.SendAsync("ReceiveMessage", message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SendMessage ex : {Message}", ex.Message);
            }
        }
    }
}
