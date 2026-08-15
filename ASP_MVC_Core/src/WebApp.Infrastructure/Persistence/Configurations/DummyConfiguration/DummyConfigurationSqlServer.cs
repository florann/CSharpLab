using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Domain.Entities;
using WebApp.Infrastructure.Interfaces;

namespace WebApp.Infrastructure.Persistence.Configurations.DummyConfiguration.SqlServer
{
    public class DummyConfigurationSqlServer : DummyConfiguration, ISqlServerEntityConfiguration
    {
        public override void Configure(EntityTypeBuilder<Dummy> builder)
        {
            base.Configure(builder);

            builder.Property(entity => entity.CreationDate)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();
        }
    }
}
