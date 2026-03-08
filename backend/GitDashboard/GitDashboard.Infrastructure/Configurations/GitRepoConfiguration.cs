using CodeEditor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeEditor.Infrastructure.Configurations
{
    public class GitRepoConfiguration : IEntityTypeConfiguration<GitRepo>
    {
        public void Configure(EntityTypeBuilder<GitRepo> builder)
        {
            builder.ToTable("git_repos");

            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(entity => entity.OwnerName)
                .IsRequired()
                .HasColumnName("owner_name")
                .HasMaxLength(255);

            builder.Property(entity => entity.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(255);

            builder.Property(entity => entity.Url)
                .IsRequired()
                .HasColumnName("url")
                .HasMaxLength(500);

            builder.Property(entity => entity.LastUpdateDate)
                .IsRequired()
                .HasColumnName("last_update_date");

            builder.Property(entity => entity.IdGitFeed)
                .IsRequired()
                .HasColumnName("id_git_feed");

            builder.HasOne(entity => entity.GitFeed)
                .WithOne(gitFeedEntity => gitFeedEntity.GitRepository)
                .HasForeignKey<GitRepo>(entity => entity.IdGitFeed);
        }
    }
}
