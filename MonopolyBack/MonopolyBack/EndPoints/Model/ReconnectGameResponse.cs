namespace MonopolyBack.EndPoints.Model;

public record class ReconnectGameResponse(Guid GameId, string Status, List<GamePlayerDto> Players);