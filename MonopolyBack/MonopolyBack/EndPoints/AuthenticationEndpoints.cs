using MonopolyBack.Application.Auth.Login;
using MonopolyBack.Application.Auth.Logout;
using MonopolyBack.EndPoints.Model;

namespace MonopolyBack.EndPoints;

public static class AuthenticationEndpoints
{
    public static WebApplication UseAuthenticationEndpoints(this WebApplication app)
    {
        var auth = app.MapGroup("/auth");

        auth.MapPost("/login", async (
                LoginRequest loginRequest,
                LoginUseCase loginUseCase,
                CancellationToken cancellationToken) =>
            {
                var command = new LoginCommand(loginRequest.Username, loginRequest.Password);
                var result = await loginUseCase.ExecuteAsync(command, cancellationToken);

                return result.IsSuccess
                    ? Results.Ok(new LoginResponse(result.AccessToken!))
                    : Results.Unauthorized();
            })
            .WithName("Login");

        auth.MapPost("/logout", async (
                HttpRequest request,
                LogoutUseCase logoutUseCase,
                CancellationToken cancellationToken) =>
            {
                var accessToken = ExtractBearerToken(request);

                if (accessToken is null)
                {
                    return Results.Unauthorized();
                }

                var result = await logoutUseCase.ExecuteAsync(new LogoutCommand(accessToken), cancellationToken);

                return result.IsSuccess
                    ? Results.Ok()
                    : Results.Unauthorized();
            })
            .WithName("Logout");

        return app;
    }

    private static string? ExtractBearerToken(HttpRequest request)
    {
        const string bearerPrefix = "Bearer ";
        var authorizationHeader = request.Headers.Authorization.ToString();

        if (!authorizationHeader.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        return authorizationHeader[bearerPrefix.Length..].Trim();
    }
}
