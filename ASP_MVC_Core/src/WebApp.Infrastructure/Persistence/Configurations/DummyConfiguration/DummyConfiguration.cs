using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Domain.Entities;
using WebApp.Infrastructure.Interfaces;

namespace WebApp.Infrastructure.Persistence.Configurations.DummyConfiguration
{
    public class DummyConfiguration : IEntityTypeConfiguration<Dummy>, IBaseEntityConfiguration
    {
        public virtual void Configure(EntityTypeBuilder<Dummy> builder)
        {
            builder.ToTable("dummyTable");

            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .ValueGeneratedOnAdd();

            builder.Property(entity => entity.Field)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}
