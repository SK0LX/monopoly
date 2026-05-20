namespace MonopolyBack.Domain.Model;

public sealed class Game
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CreatorUserId { get; set; }
    public User? CreatorUser { get; set; }
    public GameStatus Status { get; set; } = GameStatus.Waiting;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? StartedAtUtc { get; set; }
    public DateTime? FinishedAtUtc { get; set; }
    public List<GamePlayer> Players { get; set; } = new();
}