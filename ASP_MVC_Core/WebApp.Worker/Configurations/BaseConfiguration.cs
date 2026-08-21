using Cronos;

namespace WebApp.Worker.Configurations
{
    public class BaseConfiguration
    {
        public required string CronTabExpression { get; set; }
        public required bool IsActive { get; set; }
    }
}
