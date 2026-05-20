using System.Security.Cryptography;
using MonopolyBack.Application.Abstractions.Auth;

namespace MonopolyBack.Infrastructure.Auth;

public sealed class AccessTokenGenerator : IAccessTokenGenerator
{
    public string Generate()
    {
        var bytes = RandomNumberGenerator.GetBytes(32);

        return Convert.ToBase64String(bytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }
}
