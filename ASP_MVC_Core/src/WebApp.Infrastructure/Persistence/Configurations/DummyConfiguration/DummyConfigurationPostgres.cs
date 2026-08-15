using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Domain.Entities;
using WebApp.Infrastructure.Interfaces;

namespace WebApp.Infrastructure.Persistence.Configurations.DummyConfiguration
{
    public class DummyConfigurationPostgres : DummyConfiguration, IPostgresEntityConfiguration
    {
        public override void Configure(EntityTypeBuilder<Dummy> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.CreationDate)
              .HasColumnType("timestamptz")
              .HasDefaultValueSql("now()")
              .IsRequired();
        }
    }
}
