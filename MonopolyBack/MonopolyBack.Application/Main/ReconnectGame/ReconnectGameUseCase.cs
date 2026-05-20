using MonopolyBack.Application.Abstractions.Persistence;
using MonopolyBack.Domain.Model;

namespace MonopolyBack.Application.Main.ReconnectGame;

public class ReconnectGameUseCase
{
    private readonly IGameRepository _gameRepository;

    public ReconnectGameUseCase(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<ReconnectGameResult> ExecuteAsync(ReconnectGameCommand command, Guid userId, CancellationToken cancellationToken)
    {
        var game = await _gameRepository.GetByIdWithPlayersAsync(command.GameId, cancellationToken);
        if (game is null)
            return new ReconnectGameResult(false, null, "Game not found");

        if (!game.Players.Any(p => p.UserId == userId))
            return new ReconnectGameResult(false, null, "User is not a participant");

        // Можно дополнительно проверить статус игры (Waiting, InProgress)
        return new ReconnectGameResult(true, game, null);
    }
}