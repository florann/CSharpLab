using CodeEditor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeEditor.Infrastructure.Configurations
{
    public class UserGitRepoConfiguration : IEntityTypeConfiguration<UserGitRepo>
    {
        public void Configure(EntityTypeBuilder<UserGitRepo> builder)
        {
            builder.ToTable("user_git_repo");

            builder.HasKey(entity => new 
                {
                    entity.UserId, 
                    entity.GitRepoId
                }
            ); 

            builder.Property(entity => entity.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(entity => entity.GitRepoId)
                .HasColumnName("git_repo_id")
                .IsRequired();

            builder.HasOne(entity => entity.User)
                .WithMany(userEntity => userEntity.UserGitRepos)
                .HasForeignKey(entity => entity.UserId);

            builder.HasOne(entity => entity.GitRepo)
                .WithMany()
                .HasForeignKey(entity => entity.GitRepoId);
        }
    }
}
