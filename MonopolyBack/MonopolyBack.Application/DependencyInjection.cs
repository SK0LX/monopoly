using Microsoft.Extensions.DependencyInjection;
using MonopolyBack.Application.Auth.Login;
using MonopolyBack.Application.Auth.Logout;
using MonopolyBack.Application.Main.CreateGame;
using MonopolyBack.Application.Main.EndGame;
using MonopolyBack.Application.Main.ReconnectGame;

namespace MonopolyBack.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<LoginUseCase>();
        services.AddScoped<LogoutUseCase>();
        services.AddScoped<LoginUseCase>();
        services.AddScoped<LogoutUseCase>();
        services.AddScoped<CreateGameUseCase>();
        services.AddScoped<ReconnectGameUseCase>();
        services.AddScoped<EndGameUseCase>();
        return services;
    }
}
