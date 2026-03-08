using CodeEditor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeEditor.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(entity => entity.UserName)
                .HasColumnName("user_name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(entity => entity.Password)
                .HasColumnName("password")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(entity => entity.Guid)
                .HasColumnName("guid")
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
