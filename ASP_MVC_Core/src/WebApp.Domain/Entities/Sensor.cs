using WebApp.Domain.Entities.Base;
using WebApp.Domain.Enum;

namespace WebApp.Domain.Entities
{
    public class Sensor : BaseEntity
    {
        public Guid SensorGuid { get; set; }
        public required string Name { get; set; }
        public SensorType Type { get; set; }
        public required string Status { get; set; }
        public required string Error { get; set; }
        public int Battery { get; set; }
        public long LocationId { get; set; }
    }
}
