using Microsoft.EntityFrameworkCore;
using MonopolyBack.Application.Abstractions.Persistence;
using MonopolyBack.Domain.Model;

namespace MonopolyBack.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly MonopolyDbContext _dbContext;

    public UserRepository(MonopolyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Username == username, cancellationToken);
    }

    public Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }
}
