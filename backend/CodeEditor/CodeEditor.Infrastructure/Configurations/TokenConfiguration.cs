using CodeEditor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeEditor.Infrastructure.Configurations
{
    public class TokenConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.ToTable("tokens");

            builder.HasKey(entity => new
            {
                entity.RefreshToken,
                entity.UserId
            }
            );

            builder.Property(entity => entity.RefreshToken)
                .HasColumnName("refresh_token")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(entity => entity.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.HasOne(tokenEntity => tokenEntity.User)
                .WithOne(userEntity => userEntity.Token)
                .HasForeignKey<Token>(tokenEntity => tokenEntity.UserId);
        }
    }
}
