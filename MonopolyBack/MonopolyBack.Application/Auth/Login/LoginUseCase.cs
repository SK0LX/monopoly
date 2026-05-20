using MonopolyBack.Application.Abstractions.Auth;
using MonopolyBack.Application.Abstractions.Persistence;
using MonopolyBack.Domain.Model;

namespace MonopolyBack.Application.Auth.Login;

public class LoginUseCase
{
    private static readonly TimeSpan AccessTokenLifetime = TimeSpan.FromDays(7);

    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IAccessTokenHasher _accessTokenHasher;
    private readonly IAuthSessionRepository _authSessionRepository;

    public LoginUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IAccessTokenGenerator accessTokenGenerator,
        IAccessTokenHasher accessTokenHasher,
        IAuthSessionRepository authSessionRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _accessTokenGenerator = accessTokenGenerator;
        _accessTokenHasher = accessTokenHasher;
        _authSessionRepository = authSessionRepository;
    }

    public async Task<LoginResult> ExecuteAsync(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(command.Username, cancellationToken);

        if (user is null || !_passwordHasher.Verify(command.Password, user.PasswordHash))
        {
            return new LoginResult(false, null);
        }

        var accessToken = _accessTokenGenerator.Generate();
        var session = new AuthSession
        {
            UserId = user.Id,
            AccessTokenHash = _accessTokenHasher.Hash(accessToken),
            ExpiresAtUtc = DateTime.UtcNow.Add(AccessTokenLifetime)
        };

        await _authSessionRepository.AddAsync(session, cancellationToken);

        return new LoginResult(true, accessToken);
    }
}
