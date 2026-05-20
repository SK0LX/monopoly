using Microsoft.EntityFrameworkCore;
using MonopolyBack.Application.Abstractions.Persistence;
using MonopolyBack.Domain.Model;

namespace MonopolyBack.Infrastructure.Persistence.Repositories;

public sealed class AuthSessionRepository : IAuthSessionRepository
{
    private readonly MonopolyDbContext _dbContext;

    public AuthSessionRepository(MonopolyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(AuthSession session, CancellationToken cancellationToken)
    {
        await _dbContext.AuthSessions.AddAsync(session, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<AuthSession?> GetActiveByAccessTokenHashAsync(string accessTokenHash, CancellationToken cancellationToken)
    {
        var nowUtc = DateTime.UtcNow;
        return await _dbContext.AuthSessions
            .FirstOrDefaultAsync(s => s.AccessTokenHash == accessTokenHash
                                      && s.RevokedAtUtc == null
                                      && s.ExpiresAtUtc > nowUtc, cancellationToken);
    }

    public async Task<bool> RevokeByAccessTokenHashAsync(string accessTokenHash, CancellationToken cancellationToken)
    {
        var nowUtc = DateTime.UtcNow;
        var session = await _dbContext.AuthSessions
            .FirstOrDefaultAsync(
                item => item.AccessTokenHash == accessTokenHash
                        && item.RevokedAtUtc == null
                        && item.ExpiresAtUtc > nowUtc,
                cancellationToken);

        if (session is null)
        {
            return false;
        }

        session.RevokedAtUtc = nowUtc;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
