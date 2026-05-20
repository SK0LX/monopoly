using MonopolyBack.Application.Abstractions.Persistence;
using MonopolyBack.Domain.Model;

namespace MonopolyBack.Application.Main.EndGame;

public class EndGameUseCase
{
    private readonly IGameRepository _gameRepository;

    public EndGameUseCase(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<EndGameResult> ExecuteAsync(EndGameCommand command, Guid userId, CancellationToken cancellationToken)
    {
        var game = await _gameRepository.GetByIdAsync(command.GameId, cancellationToken);
        if (game is null)
            return new EndGameResult(false, "Game not found");

        if (game.CreatorUserId != userId)
            return new EndGameResult(false, "Only creator can end the game");

        if (game.Status == GameStatus.Finished)
            return new EndGameResult(false, "Game already finished");

        game.Status = GameStatus.Finished;
        game.FinishedAtUtc = DateTime.UtcNow;
        await _gameRepository.UpdateAsync(game, cancellationToken);
        return new EndGameResult(true, null);
    }
}