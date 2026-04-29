using Cronos;

namespace CodeEditor.Worker.Helpers
{
    public static class CronHelper
    {
        public static int CronToMilliseconds(string cronString)
        {
            var cronExpression = CronExpression.Parse(cronString);
            var now = DateTime.UtcNow;

            var next = cronExpression.GetNextOccurrence(now);
            var next2 = cronExpression.GetNextOccurrence(next!.Value);

            return Convert.ToInt32((next2!.Value - next.Value).TotalMilliseconds);
        }
    }
}
