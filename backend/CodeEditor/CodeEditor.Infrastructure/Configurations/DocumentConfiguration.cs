using CodeEditor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeEditor.Infrastructure.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("documents");

            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(entity => entity.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(entity => entity.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(entity => entity.Path)
                .HasColumnName("path")
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
