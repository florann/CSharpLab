using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Domain.Entities.Base;
using WebApp.Infrastructure.Generators;
using WebApp.Infrastructure.Persistence.Interfaces;

namespace WebApp.Infrastructure.Persistence.Configurations.DummyConfiguration
{
    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>, IEntityConfiguration where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(entity => entity.Guid)
                .ValueGeneratedOnAdd()
                .HasValueGenerator<GuidGenerator>()
                .IsRequired();

            builder.Property(entity => entity.RowVersion)
                .IsRequired();
            
            builder.Property(entity => entity.IsActive)
                .IsRequired();
        }
    }
}
