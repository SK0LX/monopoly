using Microsoft.EntityFrameworkCore;
using MonopolyBack.Application.Abstractions.Persistence;
using MonopolyBack.Domain.Model;

namespace MonopolyBack.Infrastructure.Persistence.Repositories;

public sealed class BoardCardRepository : IBoardCardRepository
{
    private readonly MonopolyDbContext _dbContext;

    public BoardCardRepository(MonopolyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<BoardCard>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.BoardCards
            .AsNoTracking()
            .OrderBy(card => card.Position)
            .ToListAsync(cancellationToken);
    }

    public Task<BoardCard?> GetByPositionAsync(int position, CancellationToken cancellationToken)
    {
        return _dbContext.BoardCards
            .AsNoTracking()
            .FirstOrDefaultAsync(card => card.Position == position, cancellationToken);
    }
}
