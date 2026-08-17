using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Domain.Entities;
using WebApp.Infrastructure.Persistence.Configurations.DummyConfiguration;

namespace WebApp.Infrastructure.Persistence.Configurations.LocationConfiguration
{
    public class LocationConfiguration : BaseEntityConfiguration<Location>
    {
        public override void Configure(EntityTypeBuilder<Location> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Latitude)
                .IsRequired();

            builder.Property(e => e.Longitude)
                .IsRequired();

            builder.Property(e => e.Altitude)
                .IsRequired();

            builder.Property(e => e.Zone)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.SensorId)
                .IsRequired();

            builder.HasOne<Sensor>()
                .WithMany()
                .HasForeignKey(e => e.SensorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
