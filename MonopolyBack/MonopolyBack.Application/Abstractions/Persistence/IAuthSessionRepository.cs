using MonopolyBack.Domain.Model;

namespace MonopolyBack.Application.Abstractions.Persistence;

public interface IAuthSessionRepository
{
    Task AddAsync(AuthSession session, CancellationToken cancellationToken);
    
    Task<AuthSession?> GetActiveByAccessTokenHashAsync(string accessTokenHash, CancellationToken cancellationToken);

    Task<bool> RevokeByAccessTokenHashAsync(string accessTokenHash, CancellationToken cancellationToken);
}
