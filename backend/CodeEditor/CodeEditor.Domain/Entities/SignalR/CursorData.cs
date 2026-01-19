namespace CodeEditor.Domain.Entities.SignalR
{
    public class CursorData
    {
        public string UserName { get; set; }
        public long Column { get; set; }
        public long Line { get; set; }
    }
}
