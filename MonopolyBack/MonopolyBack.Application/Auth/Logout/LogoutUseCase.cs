using MonopolyBack.Application.Abstractions.Auth;
using MonopolyBack.Application.Abstractions.Persistence;

namespace MonopolyBack.Application.Auth.Logout;

public class LogoutUseCase
{
    private readonly IAccessTokenHasher _accessTokenHasher;
    private readonly IAuthSessionRepository _authSessionRepository;

    public LogoutUseCase(
        IAccessTokenHasher accessTokenHasher,
        IAuthSessionRepository authSessionRepository)
    {
        _accessTokenHasher = accessTokenHasher;
        _authSessionRepository = authSessionRepository;
    }

    public async Task<LogoutResult> ExecuteAsync(LogoutCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.AccessToken))
        {
            return new LogoutResult(false);
        }

        var accessTokenHash = _accessTokenHasher.Hash(command.AccessToken);
        var isRevoked = await _authSessionRepository.RevokeByAccessTokenHashAsync(accessTokenHash, cancellationToken);

        return new LogoutResult(isRevoked);
    }
}
