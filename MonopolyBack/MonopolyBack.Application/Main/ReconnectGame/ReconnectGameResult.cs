using MonopolyBack.Domain.Model;

namespace MonopolyBack.Application.Main.ReconnectGame;

public record class ReconnectGameResult(bool IsSuccess, Game? Game, string? Error);