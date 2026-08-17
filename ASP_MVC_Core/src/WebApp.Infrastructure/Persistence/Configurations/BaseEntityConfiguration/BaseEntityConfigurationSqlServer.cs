using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Domain.Entities.Base;
using WebApp.Infrastructure.Interfaces;

namespace WebApp.Infrastructure.Persistence.Configurations.DummyConfiguration.SqlServer
{
    public class BaseEntityConfigurationSqlServer<TEntity> : BaseEntityConfiguration<TEntity>, ISqlServerEntityConfiguration where TEntity : BaseEntity
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.Property(entity => entity.CreationDate)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(entity => entity.UpdateDate)
               .HasColumnType("datetime2")
               .HasDefaultValueSql("GETUTCDATE()")
               .IsRequired();
        }
    }
}
