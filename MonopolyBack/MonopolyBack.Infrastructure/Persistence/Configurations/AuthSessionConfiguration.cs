using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonopolyBack.Domain.Model;

namespace MonopolyBack.Infrastructure.Persistence.Configurations;

public sealed class AuthSessionConfiguration : IEntityTypeConfiguration<AuthSession>
{
    public void Configure(EntityTypeBuilder<AuthSession> builder)
    {
        builder.ToTable("auth_sessions");

        builder.HasKey(session => session.Id);

        builder.Property(session => session.Id)
            .HasColumnName("id");

        builder.Property(session => session.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(session => session.AccessTokenHash)
            .HasColumnName("access_token_hash")
            .HasMaxLength(128)
            .IsRequired();

        builder.HasIndex(session => session.AccessTokenHash)
            .IsUnique();

        builder.Property(session => session.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(session => session.ExpiresAtUtc)
            .HasColumnName("expires_at_utc")
            .IsRequired();

        builder.Property(session => session.RevokedAtUtc)
            .HasColumnName("revoked_at_utc");

        builder.HasOne(session => session.User)
            .WithMany(user => user.AuthSessions)
            .HasForeignKey(session => session.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
