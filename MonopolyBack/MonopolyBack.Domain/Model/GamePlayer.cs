namespace MonopolyBack.Domain.Model;

public sealed class GamePlayer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid GameId { get; set; }
    public Game? Game { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public int Order { get; set; }
    public string? Color { get; set; }
    public DateTime JoinedAtUtc { get; set; } = DateTime.UtcNow;
}