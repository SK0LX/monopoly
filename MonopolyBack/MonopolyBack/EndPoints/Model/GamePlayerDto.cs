namespace MonopolyBack.EndPoints.Model;

public record class GamePlayerDto(Guid UserId, int Order, string? Color);