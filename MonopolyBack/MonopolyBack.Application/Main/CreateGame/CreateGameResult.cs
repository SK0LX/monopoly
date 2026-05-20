namespace MonopolyBack.Application.Main.CreateGame;

public record class CreateGameResult(bool IsSuccess, Guid? GameId, string? Error);