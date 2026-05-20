using System.Security.Cryptography;
using System.Text;
using MonopolyBack.Application.Abstractions.Auth;

namespace MonopolyBack.Infrastructure.Auth;

public sealed class Sha256AccessTokenHasher : IAccessTokenHasher
{
    public string Hash(string accessToken)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(accessToken));

        return Convert.ToHexString(bytes);
    }
}
