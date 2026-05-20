namespace MonopolyBack.Application.Abstractions.Auth;

public interface IAccessTokenGenerator
{
    string Generate();
}
