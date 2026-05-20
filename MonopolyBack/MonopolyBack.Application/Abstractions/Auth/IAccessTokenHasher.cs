namespace MonopolyBack.Application.Abstractions.Auth;

public interface IAccessTokenHasher
{
    string Hash(string accessToken);
}
