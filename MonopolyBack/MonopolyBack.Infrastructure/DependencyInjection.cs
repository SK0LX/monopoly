using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MonopolyBack.Application.Abstractions.Auth;
using MonopolyBack.Application.Abstractions.Persistence;
using MonopolyBack.Infrastructure.Auth;
using MonopolyBack.Infrastructure.Persistence;
using MonopolyBack.Infrastructure.Persistence.Repositories;

namespace MonopolyBack.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<MonopolyDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthSessionRepository, AuthSessionRepository>();
        services.AddScoped<IBoardCardRepository, BoardCardRepository>();

        services.AddSingleton<IPasswordHasher, Pbkdf2PasswordHasher>();
        services.AddSingleton<IAccessTokenGenerator, AccessTokenGenerator>();
        services.AddSingleton<IAccessTokenHasher, Sha256AccessTokenHasher>();
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IGameRepository, GameRepository>();

        return services;
    }
}
