using CodeEditor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeEditor.Infrastructure.Configurations
{
    public class GitFeedEntryConfiguration : IEntityTypeConfiguration<GitFeedEntry>
    {
        public void Configure(EntityTypeBuilder<GitFeedEntry> builder)
        {
            builder.ToTable("git_feed_entries");

            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(entity => entity.IdTag)
                .IsRequired()
                .HasColumnName("id_tag")
                .HasMaxLength(255);

            builder.Property(entity => entity.Title)
                .IsRequired()
                .HasColumnName("title")
                .HasMaxLength(255);

            builder.Property(entity => entity.LastUpdateDate)
                .IsRequired()
                .HasColumnName("last_update_date");

            builder.Property(entity => entity.AuthorName)
                .IsRequired()
                .HasColumnName("author_name")
                .HasMaxLength(255);

            builder.Property(entity => entity.Content)
                .IsRequired()
                .HasColumnName("content")
                .HasColumnType("text");

            builder.Property(entity => entity.Link)
                .IsRequired()
                .HasColumnName("link")
                .HasColumnType("text");

            builder.Property(entity => entity.GitFeedId)
                .IsRequired()
                .HasColumnName("git_feed_id");
        }
    }
}
