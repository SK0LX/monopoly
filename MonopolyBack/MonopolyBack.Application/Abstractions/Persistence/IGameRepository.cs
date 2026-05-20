using MonopolyBack.Domain.Model;

namespace MonopolyBack.Application.Abstractions.Persistence;

public interface IGameRepository
{
    Task AddAsync(Game game, CancellationToken cancellationToken);
    Task<Game?> GetByIdAsync(Guid gameId, CancellationToken cancellationToken);
    Task UpdateAsync(Game game, CancellationToken cancellationToken);
    
    Task<Game?> GetByIdWithPlayersAsync(Guid gameId, CancellationToken cancellationToken);
    
    Task<bool> IsPlayerInGameAsync(Guid gameId, Guid userId, CancellationToken cancellationToken);
    
}