namespace MonopolyBack.Application.Abstractions.Auth;

public interface ICurrentUserService
{
    Guid? GetUserId();
}