namespace MonopolyBack.Domain.Model;

public sealed class AuthSession
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }

    public User? User { get; set; }

    public string AccessTokenHash { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public DateTime ExpiresAtUtc { get; set; }

    public DateTime? RevokedAtUtc { get; set; }

    public bool IsActive(DateTime nowUtc)
    {
        return RevokedAtUtc is null && ExpiresAtUtc > nowUtc;
    }
}
