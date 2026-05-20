using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonopolyBack.Domain.Model;

namespace MonopolyBack.Infrastructure.Persistence.Configurations;

public sealed class GamePlayerConfiguration : IEntityTypeConfiguration<GamePlayer>
{
    public void Configure(EntityTypeBuilder<GamePlayer> builder)
    {
        builder.ToTable("game_players");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.GameId).HasColumnName("game_id");
        builder.Property(p => p.UserId).HasColumnName("user_id");
        builder.Property(p => p.Order).HasColumnName("order");
        builder.Property(p => p.Color).HasColumnName("color");
        builder.Property(p => p.JoinedAtUtc).HasColumnName("joined_at_utc");

        builder.HasIndex(p => new { p.GameId, p.UserId }).IsUnique();
    }
}