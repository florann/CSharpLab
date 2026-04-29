using CodeEditor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeEditor.Infrastructure.Configurations
{
    public class GitFeedConfiguration : IEntityTypeConfiguration<GitFeed>
    {
        public void Configure(EntityTypeBuilder<GitFeed> builder)
        {
            builder.ToTable("git_feeds");

            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
              .HasColumnName("id")
              .ValueGeneratedOnAdd()
              .IsRequired();

            builder.Property(entity => entity.Title)
             .IsRequired()
             .HasColumnName("title")
             .HasMaxLength(255);

            builder.Property(entity => entity.LastUpdateDate)
             .IsRequired()
             .HasColumnName("last_update_date");

            builder.HasMany(entity => entity.GitFeedEntries)
                .WithOne()
                .HasForeignKey(gitFeedEntry => gitFeedEntry.GitFeedId);
        }
    }
}
