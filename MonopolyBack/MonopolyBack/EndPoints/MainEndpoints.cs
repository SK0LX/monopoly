using MonopolyBack.Application.Main.CreateGame;
using MonopolyBack.Application.Main.EndGame;
using MonopolyBack.Application.Main.ReconnectGame;
using MonopolyBack.EndPoints.Model;
using MonopolyBack.Application.Abstractions.Auth;

namespace MonopolyBack.EndPoints;

public static class MainEndpoints
{
    public static WebApplication UseMainEndpoints(this WebApplication app)
    {
        var main = app.MapGroup("/main");

        main.MapPost("/createGame", async (
            CreateGameUseCase useCase,
            ICurrentUserService currentUser,
            CancellationToken ct) =>
        {
            var userId = currentUser.GetUserId();
            if (userId == null) return Results.Unauthorized();

            var result = await useCase.ExecuteAsync(new CreateGameCommand(), userId.Value, ct);
            return result.IsSuccess
                ? Results.Ok(new CreateGameResponse(result.GameId!.Value))
                : Results.BadRequest(result.Error);
        });

        main.MapPost("/reconnect", async (
            ReconnectGameRequest request,
            ReconnectGameUseCase useCase,
            ICurrentUserService currentUser,
            CancellationToken ct) =>
        {
            var userId = currentUser.GetUserId();
            if (userId == null) return Results.Unauthorized();

            var result = await useCase.ExecuteAsync(new ReconnectGameCommand(request.GameId), userId.Value, ct);
            if (!result.IsSuccess) return Results.BadRequest(result.Error);

            var game = result.Game!;
            var response = new ReconnectGameResponse(
                game.Id,
                game.Status.ToString(),
                game.Players.Select(p => new GamePlayerDto(p.UserId, p.Order, p.Color)).ToList()
            );
            return Results.Ok(response);
        });

        main.MapPost("/endGame", async (
            EndGameRequest request,
            EndGameUseCase useCase,
            ICurrentUserService currentUser,
            CancellationToken ct) =>
        {
            var userId = currentUser.GetUserId();
            if (userId == null) return Results.Unauthorized();

            var result = await useCase.ExecuteAsync(new EndGameCommand(request.GameId), userId.Value, ct);
            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        });

        return app;
    }
}