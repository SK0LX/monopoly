using MonopolyBack.Application.Abstractions.Auth;
using MonopolyBack.Application.Abstractions.Persistence;
using MonopolyBack.Domain.Model;

namespace MonopolyBack.Application.Main.CreateGame;

public class CreateGameUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;

    public CreateGameUseCase(IUserRepository userRepository, IGameRepository gameRepository)
    {
        _userRepository = userRepository;
        _gameRepository = gameRepository;
    }

    public async Task<CreateGameResult> ExecuteAsync(CreateGameCommand command, Guid creatorUserId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(creatorUserId, cancellationToken);
        if (user is null)
            return new CreateGameResult(false, null, "User not found");

        var game = new Game { CreatorUserId = creatorUserId };
        game.Players.Add(new GamePlayer
        {
            GameId = game.Id,
            UserId = creatorUserId,
            Order = 0
        });

        await _gameRepository.AddAsync(game, cancellationToken);
        return new CreateGameResult(true, game.Id, null);
    }
}