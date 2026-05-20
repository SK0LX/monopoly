using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonopolyBack.Domain.Model;

namespace MonopolyBack.Infrastructure.Persistence.Configurations;

public sealed class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("games");
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id).HasColumnName("id");
        builder.Property(g => g.CreatorUserId).HasColumnName("creator_user_id").IsRequired();
        builder.Property(g => g.Status).HasColumnName("status").HasConversion<string>().HasMaxLength(20);
        builder.Property(g => g.CreatedAtUtc).HasColumnName("created_at_utc");
        builder.Property(g => g.StartedAtUtc).HasColumnName("started_at_utc");
        builder.Property(g => g.FinishedAtUtc).HasColumnName("finished_at_utc");

        builder.HasMany(g => g.Players)
            .WithOne(p => p.Game)
            .HasForeignKey(p => p.GameId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}