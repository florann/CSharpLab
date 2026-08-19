using Cronos;

namespace WebApp.Worker.Base
{
    public class BaseConfiguration
    {
        public required string CronTabExpression { get; set; }
        public required bool IsActive { get; set; }
    }
}
