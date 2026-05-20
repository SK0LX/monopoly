using MonopolyBack.Domain.Model;

namespace MonopolyBack.Application.Abstractions.Persistence;

public interface IBoardCardRepository
{
    Task<IReadOnlyList<BoardCard>> GetAllAsync(CancellationToken cancellationToken);

    Task<BoardCard?> GetByPositionAsync(int position, CancellationToken cancellationToken);
}
