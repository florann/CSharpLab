using WebApp.Domain.Entities.Base;

namespace WebApp.Domain.Entities
{
    public class Location : BaseEntity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public required string Zone { get; set; }
        public long SensorId { get; set; }
    }
}
