using Microsoft.EntityFrameworkCore;
using MonopolyBack.Application.Abstractions.Persistence;
using MonopolyBack.Domain.Model;

namespace MonopolyBack.Infrastructure.Persistence.Repositories;

public sealed class GameRepository : IGameRepository
{
    private readonly MonopolyDbContext _dbContext;
    public Task AddAsync(Game game, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Game?> GetByIdAsync(Guid gameId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Game game, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Game?> GetByIdWithPlayersAsync(Guid gameId, CancellationToken cancellationToken)
    {
        return await _dbContext.Games
            .Include(g => g.Players)
            .ThenInclude(p => p.User) 
            .FirstOrDefaultAsync(g => g.Id == gameId, cancellationToken);
    }

    public Task<bool> IsPlayerInGameAsync(Guid gameId, Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}