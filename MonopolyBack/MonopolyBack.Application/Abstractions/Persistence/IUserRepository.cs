using MonopolyBack.Domain.Model;

namespace MonopolyBack.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
}
