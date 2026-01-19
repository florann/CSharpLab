using CodeEditor.Domain.Entities.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace CodeEditor.Api.Hubs
{
    public class CursorHub : Hub
    {
        private ILogger<CursorHub> _logger;

        public CursorHub(ILogger<CursorHub> logger)
        {
            _logger = logger;
        }

        public async Task GetAllCursors()
        {

        }

        public async Task SendCursorData(string username, long column, long line)
        {
            _logger.LogInformation("SendCursorData function");
            await this.Clients.Others.SendAsync("IncomingCursorData", new CursorData
            {
                UserName = username,
                Column = column,
                Line = line
            });
        }
    }
}
