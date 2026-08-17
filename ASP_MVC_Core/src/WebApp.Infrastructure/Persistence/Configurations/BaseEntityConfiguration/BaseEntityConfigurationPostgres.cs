using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Domain.Entities.Base;
using WebApp.Infrastructure.Interfaces;

namespace WebApp.Infrastructure.Persistence.Configurations.DummyConfiguration
{
    public class BaseEntityConfigurationPostgres<TEntity> : BaseEntityConfiguration<TEntity>, IPostgresEntityConfiguration where TEntity : BaseEntity
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.CreationDate)
                  .HasColumnType("timestamptz")
                  .HasDefaultValueSql("now()")
                  .IsRequired();

            builder.Property(b => b.UpdateDate)
               .HasColumnType("timestamptz")
               .HasDefaultValueSql("now()")
               .IsRequired();
        }
    }
}
