using Microsoft.EntityFrameworkCore;
using MonopolyBack.Domain.Model;

namespace MonopolyBack.Infrastructure.Persistence;

public sealed class MonopolyDbContext : DbContext
{
    public MonopolyDbContext(DbContextOptions<MonopolyDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Game> Games => Set<Game>();
    public DbSet<GamePlayer> GamePlayers => Set<GamePlayer>();

    public DbSet<AuthSession> AuthSessions => Set<AuthSession>();

    public DbSet<BoardCard> BoardCards => Set<BoardCard>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MonopolyDbContext).Assembly);
    }
}
