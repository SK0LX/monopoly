using Microsoft.AspNetCore.Http;
using MonopolyBack.Application.Abstractions.Auth;
using MonopolyBack.Application.Abstractions.Persistence;

namespace MonopolyBack.Infrastructure.Auth;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    public AuthenticationMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context, IAccessTokenHasher hasher, IAuthSessionRepository sessionRepo)
    {
        var token = ExtractBearerToken(context.Request);
        if (token != null)
        {
            var hash = hasher.Hash(token);
            var session = await sessionRepo.GetActiveByAccessTokenHashAsync(hash, CancellationToken.None);
            if (session != null)
            {
                context.Items["UserId"] = session.UserId;
            }
        }
        await _next(context);
    }

    private static string? ExtractBearerToken(HttpRequest request)
    {
        const string bearerPrefix = "Bearer ";
        var authHeader = request.Headers["Authorization"].ToString();
        if (!authHeader.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase))
            return null;
        return authHeader[bearerPrefix.Length..].Trim();
    }
}