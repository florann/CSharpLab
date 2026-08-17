using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Domain.Entities;
using WebApp.Infrastructure.Persistence.Configurations.DummyConfiguration;

namespace WebApp.Infrastructure.Persistence.Configurations.SensorConfiguratiokn
{
    public class SensorConfiguration : BaseEntityConfiguration<Sensor>
    {
        public override void Configure(EntityTypeBuilder<Sensor> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.SensorGuid)
                .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Type)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Error)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.Battery)
                .IsRequired();

            builder.Property(e => e.LocationId)
                .IsRequired();

            builder.HasOne<Location>()
                .WithMany()
                .HasForeignKey(e => e.LocationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
